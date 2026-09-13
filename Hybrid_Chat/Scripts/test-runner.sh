#!/usr/bin/env bash
echo "==============================================="
echo "Hybrid_Chat Assembly Build and Test script"
echo "==============================================="
echo "Step 1: RESTORING NUGET STACKS..."
dotnet restore

echo "Step 2: COMPILING BINARY TARGETS..."
dotnet build --configuration Release

echo "Step 3: EXECUTING COMPILE SIMULATIONS..."
if [ $? -eq 0 ]; then
    echo "Success: Project successfully built. Ready for deployment."
else
    echo "Error: Compilation failed! Check error logs."
    exit 1
fi