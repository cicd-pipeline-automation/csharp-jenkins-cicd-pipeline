#!/usr/bin/env bash
set -e

echo "=========================================="
echo " 🔧 Running Pre-Build Checks"
echo "=========================================="

# Display .NET SDK information
dotnet --info

echo "=========================================="
echo " ✅ Pre-Build Checks Completed"
echo "=========================================="
