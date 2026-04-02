# Read existing JSON
$filePath = 'c:\AI_Automation\TestData\EditNewCreatedPolicy_TestData 2.json'
$existingJson = Get-Content $filePath -Raw
$existingData = $existingJson | ConvertFrom-Json
$existingIds = $existingData.testCases | ForEach-Object { $_.testCaseId }

# New IE test cases (only those with 3rd editNewCreatedPolicy)
$newCases = @()

# TC_28013606
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28013606"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_IntranetZone_DontRunAntimalwareProgramsAgainstActiveXControls_Win365"
    description = "Edit profile - Don't run antimalware programs against ActiveX controls (IE Intranet Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Don't run antimalware programs against ActiveX controls"
        parentDropDownOption = "Not configured"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Intranet Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Intranet Zone"
    }
}

# TC_28014391
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28014391"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LocalMachineZone_DontRunAntimalwareProgramsAgainstActiveXControls_Win365"
    description = "Edit profile - Don't run antimalware programs against ActiveX controls (IE Local Machine Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Don't run antimalware programs against ActiveX controls"
        parentDropDownOption = "Not configured"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Local Machine Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Local Machine Zone"
    }
}

# TC_28037152
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28037152"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LockedDownInternetZone_TurnOnSmartScreenFilterScan_Win365"
    description = "Edit profile - Turn on SmartScreen Filter scan (IE Locked-Down Internet Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Turn on SmartScreen Filter scan"
        parentDropDownOption = "Not configured"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Internet Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Internet Zone"
    }
}

# TC_28037201
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28037201"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LockedDownIntranetZone_JavaPermissions_Win365"
    description = "Edit profile - Java permissions (IE Locked-Down Intranet Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Java permissions"
        parentDropDownOption = "Enabled"
        childDropDown = "Java permissions"
        childDropDownOption = "Custom"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Intranet Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Intranet Zone"
    }
}

# TC_28037225
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28037225"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LockedDownRestrictedSitesZone_TurnOnSmartScreenFilterScan_Win365"
    description = "Edit profile - Turn on SmartScreen Filter scan (IE Locked-Down Restricted Sites Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Turn on SmartScreen Filter scan"
        parentDropDownOption = "Not configured"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Restricted Sites Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Restricted Sites Zone"
    }
}

# TC_28037231
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28037231"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LockedDownLocalMachineZone_JavaPermissions_Win365"
    description = "Edit profile - Java permissions (IE Locked-Down Local Machine Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Java permissions"
        parentDropDownOption = "Enabled"
        childDropDown = "Java permissions"
        childDropDownOption = "Custom"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Local Machine Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Local Machine Zone"
    }
}

# TC_28037233
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28037233"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LockedDownTrustedSitesZone_JavaPermissions_Win365"
    description = "Edit profile - Java permissions (IE Locked-Down Trusted Sites Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Java permissions"
        parentDropDownOption = "Enabled"
        childDropDown = "Java permissions"
        childDropDownOption = "Custom"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Trusted Sites Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Trusted Sites Zone"
    }
}

# TC_28037240
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28037240"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_TrustedSitesZone_DontRunAntimalwareProgramsAgainstActiveXControls_Win365"
    description = "Edit profile - Don't run antimalware programs against ActiveX controls (IE Trusted Sites Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Don't run antimalware programs against ActiveX controls"
        parentDropDownOption = "Not configured"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Trusted Sites Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Trusted Sites Zone"
    }
}

# TC_28766165
$newCases += [PSCustomObject]@{
    testCaseId = "TC_28766165"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_AllowFallbackToSSL3InternetExplorer_Win365"
    description = "Edit profile - Allow fallback to SSL 3.0 (Internet Explorer) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Allow fallback to SSL 3.0 (Internet Explorer)"
        parentDropDownOption = "Disabled"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer"
        childSectionPath = "Windows Components > Internet Explorer"
    }
}

# TC_29903229
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29903229"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_AdvancedPage_TurnOffEncryptionSupport_Win365"
    description = "Edit profile - Turn off encryption support (IE Advanced Page) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Turn off encryption support"
        parentDropDownOption = "Enabled"
        childDropDown = "Secure Protocol combinations"
        childDropDownOption = "Only use SSL 3.0"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Advanced Page"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Advanced Page"
    }
}

# TC_29903530
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29903530"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_PreventManagingSmartScreenFilter_Win365"
    description = "Edit profile - Prevent managing SmartScreen Filter (IE) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Prevent managing SmartScreen Filter"
        parentDropDownOption = "Not configured"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer"
        childSectionPath = "Windows Components > Internet Explorer"
    }
}

# TC_29949389
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29949389"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_TrustedSitesZone_InitializeAndScriptActiveXControlsNotMarkedAsSafe_Win365"
    description = "Edit profile - Initialize and script ActiveX controls not marked as safe (IE Trusted Sites Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Initialize and script ActiveX controls not marked as safe"
        parentDropDownOption = "Disabled"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Trusted Sites Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Trusted Sites Zone"
    }
}

# TC_29949431
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29949431"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_TrustedSitesZone_JavaPermissions_Win365"
    description = "Edit profile - Java permissions (IE Trusted Sites Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Java permissions"
        parentDropDownOption = "Enabled"
        childDropDown = "Java permissions"
        childDropDownOption = "Custom"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Trusted Sites Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Trusted Sites Zone"
    }
}

# TC_29949521
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29949521"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LockedDownRestrictedSitesZone_JavaPermissions_Win365"
    description = "Edit profile - Java permissions (IE Locked-Down Restricted Sites Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Java permissions"
        parentDropDownOption = "Enabled"
        childDropDown = "Java permissions"
        childDropDownOption = "Custom"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Restricted Sites Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Locked-Down Restricted Sites Zone"
    }
}

# TC_29949657
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29949657"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_LocalMachineZone_JavaPermissions_Win365"
    description = "Edit profile - Java permissions (IE Local Machine Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Java permissions"
        parentDropDownOption = "Enabled"
        childDropDown = "Java permissions"
        childDropDownOption = "Custom"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Local Machine Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Local Machine Zone"
    }
}

# TC_29949711
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29949711"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_IntranetZone_InitializeAndScriptActiveXControlsNotMarkedAsSafe_Win365"
    description = "Edit profile - Initialize and script ActiveX controls not marked as safe (IE Intranet Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Initialize and script ActiveX controls not marked as safe"
        parentDropDownOption = "Disabled"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Intranet Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Intranet Zone"
    }
}

# TC_29949760
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29949760"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_IntranetZone_JavaPermissions_Win365"
    description = "Edit profile - Java permissions (IE Intranet Zone) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Java permissions"
        parentDropDownOption = "Enabled"
        childDropDown = "Java permissions"
        childDropDownOption = "Custom"
        parentSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Local Machine Zone"
        childSectionPath = "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Local Machine Zone"
    }
}

# TC_29950029
$newCases += [PSCustomObject]@{
    testCaseId = "TC_29950029"
    testName = "Edit_AdministrativeTemplates_InternetExplorer_AllowFallbackToSSL3SecurityFeatures_Win365"
    description = "Edit profile - Allow fallback to SSL 3.0 (IE Security Features) - 3rd edit instance"
    category = "Windows 365 Security Baseline"
    priority = "High"
    enabled = $true
    parameters = [PSCustomObject]@{
        firstLink = "Endpoint security"
        secondLink = "Security baselines"
        secBaselineName = "Windows 365 Security Baseline"
        configurationSettings = "Administrative Templates"
        parentDropDown = "Allow fallback to SSL 3.0 (Internet Explorer)"
        parentDropDownOption = "Disabled"
        childDropDown = ""
        childDropDownOption = ""
        parentSectionPath = "Windows Components > Internet Explorer > Security Features"
        childSectionPath = "Windows Components > Internet Explorer > Security Features"
    }
}

# Filter out already existing entries
$casesToAdd = @()
foreach ($c in $newCases) {
    if ($existingIds -notcontains $c.testCaseId) {
        $casesToAdd += $c
    } else {
        Write-Host "Skipping existing: $($c.testCaseId)"
    }
}

# Merge
$allCases = @()
$allCases += $existingData.testCases
$allCases += $casesToAdd

$wrapper = [PSCustomObject]@{
    testCases = $allCases
}

$json = $wrapper | ConvertTo-Json -Depth 4
# Fix unicode escapes
$json = $json -replace '\\u003e', '>' -replace '\\u0027', "'" -replace '\\u003c', '<'

$json | Out-File -FilePath $filePath -Encoding UTF8
Write-Host "Done. Added $($casesToAdd.Count) IE entries. Total: $($allCases.Count) test cases."
