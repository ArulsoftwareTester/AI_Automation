$jsonPath = "C:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy\TestData_AppReggersion\WinClassicApp_Sanity_BACKUP.json"
$json = Get-Content $jsonPath -Raw | ConvertFrom-Json

# Build lookup: first occurrence of each testCaseId -> index
$lookup = @{}
for ($i = 0; $i -lt $json.testCases.Count; $i++) {
    $id = $json.testCases[$i].testCaseId
    if (-not $lookup.ContainsKey($id)) { $lookup[$id] = $i }
}

# Find all indices for duplicate TC_4180341C
$tc41C_idx = @()
for ($i = 0; $i -lt $json.testCases.Count; $i++) {
    if ($json.testCases[$i].testCaseId -eq "TC_4180341C") { $tc41C_idx += $i }
}

function SetDep($tcId, $value) {
    if ($lookup.ContainsKey($tcId)) {
        $json.testCases[$lookup[$tcId]].parameters.Dependencies = $value
    }
}
function SetSup($tcId, $value) {
    if ($lookup.ContainsKey($tcId)) {
        $json.testCases[$lookup[$tcId]].parameters.Supercedence = $value
    }
}
function GetName($tcId) {
    if ($lookup.ContainsKey($tcId)) { return $json.testCases[$lookup[$tcId]].parameters.Name }
    return ""
}

# ====== DEPENDENCY TESTS ======

# TC_4180340: A->B (Available,UserGroup)
SetDep "TC_4180340B" ""  # B = dependency, no dep itself
# TC_4180340A already has correct value

# TC_4180341: A->B then A->C
SetDep "TC_4180341B" ""  # A: initially no dep in JSON, deps added at runtime
if ($tc41C_idx.Count -ge 2) {
    $json.testCases[$tc41C_idx[0]].parameters.Dependencies = ""  # B
    $json.testCases[$tc41C_idx[1]].parameters.Dependencies = ""  # C
}
$bName41 = if($tc41C_idx.Count -ge 1){$json.testCases[$tc41C_idx[0]].parameters.Name}else{""}
SetDep "TC_4180341B" $bName41

# TC_4180343: Dependency Loop (A->B->A fails)
SetDep "TC_4180343B" ""
SetDep "TC_4180343A" (GetName "TC_4180343B")

# TC_4180345: A->B(EXE), A->C(MSI)
SetDep "TC_4180345B" ""
SetDep "TC_4180345C" ""
$b345 = GetName "TC_4180345B"; $c345 = GetName "TC_4180345C"
SetDep "TC_4180345A" "$b345,$c345"

# TC_4180346: A->B (AutoInstall=No), B absent, A fails
SetDep "TC_4180346" "NonExistentApp_AutoInstallNo"

# TC_4180347: A->B(no),A->C(yes),A->D(no),A->E(yes)
SetDep "TC_4180347" "MultiDep: B(autoInstall=no,installed), C(autoInstall=yes), D(autoInstall=no,installed), E(autoInstall=yes)"

# TC_4180358: A->B, B->C chain
SetDep "TC_4180358C" ""
SetDep "TC_4180358B" (GetName "TC_4180358C")
SetDep "TC_4180358A" (GetName "TC_4180358B")

# TC_4180360: A->B, A already installed
SetDep "TC_4180360B" ""
SetDep "TC_4180360A" (GetName "TC_4180360B")

# TC_4180361: A->B, B updated to v2
SetDep "TC_4180361B" ""
SetDep "TC_4180361A" (GetName "TC_4180361B")

# TC_4180362: A->B, B fails A not attempted
SetDep "TC_4180362B" ""
SetDep "TC_4180362A" (GetName "TC_4180362B")

# TC_4180363: A->B, A updated to v2
SetDep "TC_4180363B" ""
SetDep "TC_4180363A" (GetName "TC_4180363B")

# TC_4185879: A->B both succeed
SetDep "TC_4185879B" ""
SetDep "TC_4185879A" (GetName "TC_4185879B")

# TC_4185880: A->B conflict (A=Available, B=Uninstall)
SetDep "TC_4185880B" ""
SetDep "TC_4185880A" (GetName "TC_4185880B")

# TC_4185914: Can add dependency (UI)
SetDep "TC_4185914B" ""
SetDep "TC_4185914A" (GetName "TC_4185914B")

# TC_4185926: Can delete dependency (UI)
SetDep "TC_4185926B" ""
SetDep "TC_4185926A" (GetName "TC_4185926B")

# TC_4237713: A->B (Required,DeviceGroup)
SetDep "TC_4237713B" ""
SetDep "TC_4237713A" (GetName "TC_4237713B")

# TC_4237714: A->B then A->C (Available,UserGroup)
SetDep "TC_4237714B" ""
SetDep "TC_4237714C" ""
SetDep "TC_4237714A" (GetName "TC_4237714B")

# TC_4237715: A->B, B fails A not attempted (Available)
SetDep "TC_4237715B" ""
SetDep "TC_4237715A" (GetName "TC_4237715B")

# ====== SUPERSEDENCE TESTS ======
# Notation: A <<- B means B supersedes A (AppUpdate, no uninstall)
#           A <<+ B means B supersedes A (AppReplace, uninstall previous)
# B is new app, A is old app. B.Supercedence = A's name

# TC_8195617: Delete superseded app (UI)
SetSup "TC_8195617A" ""  # A = old/superseded
SetSup "TC_8195617B" (GetName "TC_8195617A")  # B supersedes A

# TC_8195618: Delete superseding app (UI)
SetSup "TC_8195618B" ""  # B = old/superseded
SetSup "TC_8195618A" (GetName "TC_8195618B")  # A supersedes B

# TC_8195638: A<<-B (AppUpdate) A not present, only B installed
SetSup "TC_8195638A" ""
SetSup "TC_8195638B" (GetName "TC_8195638A")

# TC_8195641: A<<-B (AppUpdate) A not present, B present
SetSup "TC_8195641A" ""
SetSup "TC_8195641B" (GetName "TC_8195641A")

# TC_8195646: A<<+B (AppReplace) A not present, Install B
SetSup "TC_8195646A" ""
SetSup "TC_8195646B" (GetName "TC_8195646A")

# TC_8195647: A<<+B (AppReplace) A present, A uninstalled, B installed
SetSup "TC_8195647A" ""
SetSup "TC_8195647B" (GetName "TC_8195647A")

# Add "Uninstall previous version" for supersedence tests
$updateTests = @("TC_8195638B","TC_8195641B","TC_8195617B")  # AppUpdate = No
$replaceTests = @("TC_8195646B","TC_8195647B")  # AppReplace = Yes

foreach ($tcId in $updateTests) {
    if ($lookup.ContainsKey($tcId)) {
        $json.testCases[$lookup[$tcId]].parameters | Add-Member -NotePropertyName "Uninstall previous version" -NotePropertyValue "No" -Force
    }
}
foreach ($tcId in $replaceTests) {
    if ($lookup.ContainsKey($tcId)) {
        $json.testCases[$lookup[$tcId]].parameters | Add-Member -NotePropertyName "Uninstall previous version" -NotePropertyValue "Yes" -Force
    }
}

# Save
$json | ConvertTo-Json -Depth 10 | Set-Content $jsonPath -Encoding UTF8
Write-Host "Saved updated JSON with $($json.testCases.Count) test cases"

# Print summary
Write-Host "`n=== DEPENDENCY MAPPINGS ==="
foreach ($tc in $json.testCases) {
    $d = $tc.parameters.Dependencies
    if ($d -and $d -ne "" -and $d -ne "Configured as per test description") {
        Write-Host "$($tc.testCaseId) | Dep=[$d]"
    }
}
Write-Host "`n=== SUPERSEDENCE MAPPINGS ==="
foreach ($tc in $json.testCases) {
    $s = $tc.parameters.Supercedence
    $u = $tc.parameters.'Uninstall previous version'
    if ($s -and $s -ne "" -and $s -ne "Configured as per test description") {
        $line = "$($tc.testCaseId) | Sup=[$s]"
        if ($u) { $line += " | UninstallPrev=$u" }
        Write-Host $line
    }
}
