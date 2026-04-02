#!/usr/bin/env swift

import Foundation
import XCTest

// Swift script to tap Devices button using XCUITest on physical device
// Usage: swift TapDevicesButton.swift <deviceId> <bundleId>

class CompanyPortalUITest: XCTestCase {
    func testTapDevicesButton() {
        let app = XCUIApplication()
        app.launch()
        
        // Wait for the app to load
        sleep(3)
        
        // Try to find and tap the Devices button
        let devicesButton = app.buttons["Devices"]
        if devicesButton.exists {
            print("Found Devices button")
            devicesButton.tap()
            print("Tapped Devices button successfully")
        } else {
            // Try tab bar
            let tabBar = app.tabBars.element
            let devicesTab = tabBar.buttons["Devices"]
            if devicesTab.exists {
                print("Found Devices tab in tab bar")
                devicesTab.tap()
                print("Tapped Devices tab successfully")
            } else {
                print("ERROR: Could not find Devices button or tab")
                print("Available buttons:")
                for button in app.buttons.allElementsBoundByIndex {
                    print("  - \(button.label)")
                }
            }
        }
    }
}

// Run the test
let test = CompanyPortalUITest()
test.testTapDevicesButton()
