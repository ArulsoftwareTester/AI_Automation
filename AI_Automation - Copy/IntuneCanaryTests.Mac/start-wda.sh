#!/bin/bash

# Script to start WebDriverAgent on iOS device
# Usage: ./start-wda.sh <device-udid>

DEVICE_UDID=$1

if [ -z "$DEVICE_UDID" ]; then
    echo "Error: Device UDID is required"
    echo "Usage: $0 <device-udid>"
    exit 1
fi

echo "Starting WebDriverAgent on device: $DEVICE_UDID"
echo "========================================"

# Find WebDriverAgent location
WDA_PATH="$HOME/.appium/node_modules/appium-xcuitest-driver/node_modules/appium-webdriveragent"

if [ ! -d "$WDA_PATH" ]; then
    echo "Error: WebDriverAgent not found at $WDA_PATH"
    echo ""
    echo "Please install it with:"
    echo "  cd ~/.appium/node_modules/appium-xcuitest-driver"
    echo "  npm install"
    exit 1
fi

echo "Found WebDriverAgent at: $WDA_PATH"
cd "$WDA_PATH"

# Create a log file
LOG_FILE="/tmp/wda-$DEVICE_UDID.log"
echo "Logs will be written to: $LOG_FILE"

# Check if WDA is already running
WDA_PID=$(ps aux | grep "WebDriverAgentRunner" | grep "$DEVICE_UDID" | grep -v grep | awk '{print $2}')
if [ ! -z "$WDA_PID" ]; then
    echo "WebDriverAgent is already running (PID: $WDA_PID)"
    echo "To restart, kill the process first: kill $WDA_PID"
    exit 0
fi

# Start WebDriverAgent
echo ""
echo "Starting WebDriverAgent..."
echo "This will keep running - press Ctrl+C to stop"
echo "========================================"

xcodebuild -project WebDriverAgent.xcodeproj \
           -scheme WebDriverAgentRunner \
           -destination "id=$DEVICE_UDID" \
           -allowProvisioningUpdates \
           test 2>&1 | tee "$LOG_FILE"
