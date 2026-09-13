# Hybrid_Chat System Architecture & Design Documentation

Hybrid_Chat is a production-grade C# Windows Forms (.NET 8.0-windows) application showcasing a **hybrid TCP/UDP architecture** for real-time chat, continuous heartbeat synchronization, and instant typing telemetry. 

Designed with strict Human-Computer Interaction (HCI) standards and professional thread safety, it includes a split-pane control cockpit that lets users run and monitor both the **Chat Server** and **Chat Client** concurrently on a single dashboard, or deploy them separately.

## 1. Architectural Patterns

```
+-------------------------------------------------------------------------+
|                         UI Layer (WinForms Dashboard)                   |
|       - Server Monitor Controls        - Chat Room Panel                |
|       - Active Client DataGrid         - Sync Telemetry Feed            |
+------------------------------------+------------------------------------+
                                     | Invokes with SynchronizationContext
                                     v
+-------------------------------------------------------------------------+
|                       Network Coordinator Services                      |
|                                                                         |
|   +----------------------------------+ +----------------------------+   |
|   | TCP Core Service (Reliable)      | | UDP Presence Service       |   |
|   | - Client Handshake               | | - Typing Indicator Sync    |   |
|   | - Message Broadcasts             | | - Active Presence Beacon   |   |
|   +----------------------------------+ +----------------------------+   |
+------------------------------------+------------------------------------+
                                     | Data Streams
                                     v
+-------------------------------------------------------------------------+
|                            Data & Log Layer                             |
|  - Concurrent Session Map   - Serialized Chat Frames   - Audit Logs     |
+-------------------------------------------------------------------------+
```

### Core Network Protocols
1. **TCP (Transmission Control Protocol)**: Used for guaranteed, in-order packet delivery. Handles client connections, disconnections, user registrations, text messaging, and system-wide notifications.
2. **UDP (User Datagram Protocol)**: Used for high-frequency, low-overhead, fire-and-forget events. Handles real-time typing indicators (`IsTyping = true/false`) and automated presence/discovery beacons to keep the system responsive without exhausting connection pools.

---

## 2. Component Details

### A. SyncChatServer (`Services/SyncChatServer.cs`)
- **TcpListener Core**: Runs in a dedicated background task. Accepts incoming client connections and hands them off to an active `ClientSession` handler.
- **UDP Presence Listener**: Spawns an asynchronous receiver socket listening on port `11001` to digest real-time typing indicators and presence heartbeats.
- **Session Manager**: Manages a thread-safe `ConcurrentDictionary<string, ClientSession>` holding open TCP client streams, tracking user credentials, and routing broadcasts.

### B. SyncChatClient (`Services/SyncChatClient.cs`)
- **Asynchronous TCP Stream Reader**: Monitors incoming commands from the server (`MSG`, `JOIN`, `LEAVE`, `SYS`) on a background loop, raising events back to the UI thread safely.
- **UDP Telemetry Broadcaster**: Broadcasts low-latency status packets (`TYPING:Username:1` or `TYPING:Username:0`) to the server as the user interacts with the UI.

### C. Human-Computer Interaction (HCI) & Synchronized UI
- **Thread-Safe Cross-Thread UI Updates**: Implements careful `InvokeRequired` checking and UI dispatch mechanisms ensuring incoming socket payloads do not trigger thread-access exceptions on WinForms components.
- **Responsive Interface Controls**: Utilizes custom visual states, disabling and enabling control regions dynamically based on connection status (e.g., locking username editing once registered to the server).

---

## 3. How to Setup and Run

### Prerequisites
- **.NET 8.0 SDK** or later installed.
- Windows OS (required for C# WinForms execution).
- IDE: Visual Studio 2022, JetBrains Rider, or VS Code.

### Compilation and Execution via .NET CLI
1. Extract the project workspace files into a local folder (e.g., `Hybrid_Chat`).
2. Open your favorite terminal inside that folder.
3. Restore dependencies, build the solution, and launch the binary:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project Hybrid_Chat.csproj
   ```
4. Once open:
   - Click **Start Server** on the left side (configures server on Localhost ports TCP `11000` & UDP `11001`).
   - Fill in your Username on the right side (e.g., `Alice`) and click **Connect to Server**.
   - Open a second instance of the application to simulate multi-client environments, or chat directly. Typing into the chatbox transmits real-time UDP typing indicators to the server instantly!

### Client-Only Mode
If you want a client-only UI without the server panel, launch the application with the `client` argument:

```bash
cd Hybrid_Chat
dotnet run --project Hybrid_Chat.csproj --client
```

Or with the executable once built:

```bash
Hybrid_Chat.exe --client
```

This starts the standalone client form and hides the server controls entirely.

### Responsive Resize
Both the server and client windows now adjust automatically when resized. The UI controls are anchored so the chat feed, logs, and control panels grow or shrink smoothly with the form.

### Client-Only Mode

If you want a client-only UI without the server panel, launch the application with the `client` argument:

```bash
cd Hybrid_Chat
dotnet run --project Hybrid_Chat.csproj --client
```

Or with the executable once built:

```bash
Hybrid_Chat.exe --client
```

This starts the standalone client form and hides the server controls entirely.
