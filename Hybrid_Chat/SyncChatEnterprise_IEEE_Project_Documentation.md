# Hybrid_Chat Project Documentation

## Document Information
- Title: Hybrid_Chat
- Version: 1.0
- Date: May 26, 2026
- Author: Syed M Danish (64999) & Abdul Wasay (65066)
- Project Type: Windows Forms Desktop Application
- Document Status: Draft

## Abstract
Hybrid_Chat is a secure, real-time chat application built with .NET 8.0 Windows Forms. It combines reliable TCP messaging for chat delivery with UDP telemetry for instantaneous typing indicators and connection presence. The system supports a server dashboard and client-only mode, with TLS-secured TCP connections, heartbeat keepalive, and responsive UI resizing.

## Keywords
SyncChat, TCP, UDP, WinForms, TLS, real-time chat, client-server, IEEE documentation

## 1. Introduction
### 1.1 Purpose
This document provides a technical overview of the Hybrid_Chat application, including system architecture, functional and non-functional requirements, design rationale, implementation details, testing strategy, deployment process, and security considerations.

### 1.2 Scope
The project enables secure multi-client chat communication over a local network or localhost. It is designed for demonstration and prototyping of hybrid reliable/unreliable transport patterns, system synchronization, and graphical interface separation between server and client modes.

### 1.3 Audience
- Developers extending or maintaining Hybrid_Chat
- Testers verifying functionality and performance
- System integrators deploying the application
- Project managers reviewing requirements and status

## 2. System Overview
Hybrid_Chat consists of two main runtime modes:
- **Server mode**: Displays the server dashboard with runtime controls, connection monitoring, active sessions, chat logs, and telemetry insights.
- **Client-only mode**: Launches a standalone chat client without server configuration controls.

### 2.1 System Context
The application uses:
- TCP for secure chat messaging and administrative commands
- UDP for typing telemetry and presence updates
- AES/TLS for transport encryption on TCP
- WinForms for the desktop user interface

## 3. Requirements
### 3.1 Functional Requirements
- FR1: Start and stop the chat server
- FR2: Connect clients to the server with a username
- FR3: Send and receive chat messages in real-time
- FR4: Broadcast typing state to all connected clients
- FR5: Show active client sessions and logs in server mode
- FR6: Operate in client-only mode via startup argument (`--client`)
- FR7: Automatically resize UI controls with window changes

### 3.2 Non-functional Requirements
- NFR1: Build with .NET 8.0 on Windows
- NFR2: Maintain thread-safe UI updates
- NFR3: Use secure TLS for TCP transport
- NFR4: Keep the UI responsive during network activity
- NFR5: Support multi-client operation and auto-adjust GUI layout

## 4. Definitions and Acronyms
- TCP: Transmission Control Protocol
- UDP: User Datagram Protocol
- TLS: Transport Layer Security
- UI: User Interface
- HCI: Human-Computer Interaction
- FR: Functional Requirement
- NFR: Non-functional Requirement

## 5. Architecture
### 5.1 Component Architecture
- `SyncChatServer` handles TCP listeners, client session management, TLS authentication, UDP telemetry receiver, and broadcast routing.
- `SyncChatClient` manages TCP connection initiation, message sending, UDP typing telemetry, and keepalive loops.
- `FormMain` implements the server-plus-client dashboard UI in server mode.
- `FormClient` implements a separate client-only UI.

### 5.2 Data Flow
- Client sends chat payloads over TLS-secured TCP to server
- Server broadcasts chat payloads and control events to all connected clients
- Clients send typing state via UDP to server
- Server distributes typing state updates to all clients for live indicators

## 6. Design
### 6.1 UI Design
- Server UI is built around a split container, separating server controls from client chat controls.
- Client-only UI uses a standalone form with the same chat experience but no server controls.
- UI elements are anchored to top/bottom/left/right where appropriate so windows resize fluidly.

### 6.2 Network Protocol Design
- Packet framing is handled by `NetworkPacket` with defined packet types such as `MSG`, `SYS`, `TYPING`, `PING`, and `PONG`.
- `ClientSession` wraps TLS streams and manages send locking for thread-safe writes.
- Heartbeat keepalive and session cleanup detect broken client connections.

### 6.3 Security Design
- TLS is applied to TCP connections to protect message privacy.
- Self-signed certificates are used for local development testing.
- Keepalive monitoring drops stale or disconnected sessions.

## 7. Implementation
### 7.1 Code Structure
- `Program.cs`: Application startup logic and mode selection
- `GUI/FormMain.cs`: Server dashboard and combined UI logic
- `GUI/FormClient.cs`: Client-only UI logic
- `Services/SyncChatServer.cs`: TCP/UDP server logic and session orchestration
- `Services/SyncChatClient.cs`: Client networking and telemetry
- `Models/NetworkPacket.cs`: Packet serialization and enum definitions
- `Models/ClientSession.cs`: Session state and stream handling

### 7.2 Deployment Setup
1. Open the project folder in Visual Studio or VS Code.
2. Restore dependencies and build using `dotnet build -p:UseAppHost=false`.
3. Launch server mode with `dotnet run --project Hybrid_Chat.csproj`.
4. Launch client-only mode with `dotnet run --project Hybrid_Chat.csproj --client`.

## 8. Testing
### 8.1 Manual Testing
- Verify server startup and shutdown
- Connect multiple clients and exchange messages
- Confirm typing indicators propagate across clients
- Resize both server and client windows to verify responsive GUI layout
- Validate TLS handshake and secure connection establishment

### 8.2 Recommended Test Cases
- TC1: Single client connects and sends a chat message
- TC2: Multiple clients connect, chat, and see typing indicators
- TC3: Client disconnects abruptly and server removes session
- TC4: Window resize preserves layout and control accessibility
- TC5: Launch client-only mode without server controls visible

## 9. Deployment
### 9.1 Build Command
```bash
cd Hybrid_Chat
dotnet build -p:UseAppHost=false
```

### 9.2 Run Commands
- Server mode:
```bash
dotnet run --project Hybrid_Chat.csproj
```
- Client-only mode:
```bash
dotnet run --project Hybrid_Chat.csproj --client
```

## 10. Security Considerations
- TLS encrypts chat traffic over TCP.
- The server should be run in a trusted network for local development.
- Self-signed certificates are acceptable for testing but not for production.
- Future improvements may include certificate validation and client authentication.

## 11. Maintenance and Future Work
- Add persistent chat history storage
- Add user authentication and role-based access
- Add support for remote deployment and NAT traversal
- Add formal unit tests and automated UI tests
- Replace self-signed TLS certificates with trusted PKI certificates for production

## 12. References
- IEEE Standard for Software Project Documentation
- .NET 8.0 Windows Forms documentation
- TCP and UDP networking best practices
- TLS secure communication guidelines
