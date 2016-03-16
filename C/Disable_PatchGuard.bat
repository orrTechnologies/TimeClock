@ECHO OFF

ECHO ===============================================================================
ECHO  Project : Disable Patchguard
ECHO  Version : 5.3
ECHO  Coder   : HelioS
ECHO  Site    : http://www.artificialaiming.net
ECHO ===============================================================================
ECHO.

ECHO   * Initializing.

@setlocal ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION
@cd /d "%~dp0"

set alfanum=0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz

set RandomName1=
FOR /L %%b IN (0, 1, 16) DO (
SET /A rnd_num=!RANDOM! * 62 / 32768 + 1
for /F %%c in ('echo %%alfanum:~!rnd_num!^,1%%') do set RandomName1=!RandomName1!%%c
)

set RandomName2=
FOR /L %%b IN (0, 1, 16) DO (
SET /A rnd_num=!RANDOM! * 62 / 32768 + 1
for /F %%c in ('echo %%alfanum:~!rnd_num!^,1%%') do set RandomName2=!RandomName2!%%c
)


SET BootGuid={46595952-454E-4F50-4747-554944FEEEEE}
SET SystemDirectory=%SYSTEMROOT%\System32
SET BcdEditPath=%SystemDirectory%\bcdedit.exe


ECHO   * Checking BIOS/UEFI System.

%BcdEditPath% | find "winload.efi" >NUL
if %ERRORLEVEL% == 0 goto BOOT_UEFI

%BcdEditPath% | find "winload.exe" >NUL
if %ERRORLEVEL% == 0 goto BOOT_BIOS

goto INVALID_BOOT

:BOOT_UEFI
ECHO       UEFI Detected.
SET PatchedWinloadName=winload_patched.efi
SET PatchedNtoskrnlName=ntoskrnl_patched.exe
SET NtoskrnlPatchedName=%RandomName1%.exe
SET WinLoadPatchedName=%RandomName2%.efi
SET SystemNtoskrnlPatchedPath=%SystemDirectory%\%NtoskrnlPatchedName%
SET SystemWinLoadPatchedPath=%SystemDirectory%\%WinLoadPatchedName%

GOTO CHECK_ARCHITECTURE

:BOOT_BIOS
ECHO       BIOS Detected.
SET PatchedWinloadName=winload_patched.exe
SET PatchedNtoskrnlName=ntoskrnl_patched.exe
SET NtoskrnlPatchedName=%RandomName1%.exe
SET WinLoadPatchedName=%RandomName2%.exe
SET SystemNtoskrnlPatchedPath=%SystemDirectory%\%NtoskrnlPatchedName%
SET SystemWinLoadPatchedPath=%SystemDirectory%\%WinLoadPatchedName%

GOTO CHECK_ARCHITECTURE


:CHECK_ARCHITECTURE
ECHO       Generated random names = %NtoskrnlPatchedName% - %WinLoadPatchedName%

ECHO   * Checking Operating System Architecture.

if "%PROCESSOR_ARCHITECTURE%" == "AMD64" goto CHECK_VERSION

goto INVALID_ARCHITECTURE

:CHECK_VERSION
ECHO       64bit Detected.
ECHO   * Checking Operating System Version.

ver | find "6.1.7601" >NUL
if %ERRORLEVEL% == 0 goto VERSION_WIN7

ver | find "6.3.9600" >NUL
if %ERRORLEVEL% == 0 goto VERSION_WIN8_1

ver | find "10.0.10586" >NUL
if %ERRORLEVEL% == 0 goto VERSION_WIN10

goto INVALID_VERSION




:VERSION_WIN7
ECHO       Windows 7 Detected.
SET NewBootName=Windows 7 (Patched)
SET OriginNtoskrnlPatchedPath=%CD%\Version 6.1.7601\x64\%PatchedNtoskrnlName%
SET OriginWinLoadPatchedPath=%CD%\Version 6.1.7601\x64\%PatchedWinloadName%

goto CREATE_BOOT



:VERSION_WIN8_1
ECHO       Windows 8.1 Detected.
SET NewBootName=Windows 8.1 (Patched)
SET OriginNtoskrnlPatchedPath=%CD%\Version 6.3.9600\x64\%PatchedNtoskrnlName%
SET OriginWinLoadPatchedPath=%CD%\Version 6.3.9600\x64\%PatchedWinloadName%

goto CREATE_BOOT



:VERSION_WIN10
ECHO       Windows 10 Detected.
SET NewBootName=Windows 10 (Patched)
SET OriginNtoskrnlPatchedPath=%CD%\Version 10.0.10586\x64\%PatchedNtoskrnlName%
SET OriginWinLoadPatchedPath=%CD%\Version 10.0.10586\x64\%PatchedWinloadName%

goto CREATE_BOOT



:CREATE_BOOT

ECHO   * Copying Patched Files.

IF NOT EXIST "%OriginNtoskrnlPatchedPath%" GOTO FILES_NOT_FOUND
IF NOT EXIST "%OriginWinLoadPatchedPath%" GOTO FILES_NOT_FOUND

COPY "%OriginNtoskrnlPatchedPath%" "%SystemNtoskrnlPatchedPath%"
COPY "%OriginWinLoadPatchedPath%" "%SystemWinLoadPatchedPath%"


ECHO   * Creating New Boot Entry.

for /f "tokens=2 delims={}" %%g in ('%BcdEditPath% /create -d "%NewBootName%" -application osloader') do set BootGuid={%%g}

ECHO       New Boot Entry Name = %NewBootName%
ECHO       New Boot Entry GUID = %BootGuid%

%BcdEditPath% -set %BootGuid% DEVICE PARTITION=%SYSTEMDRIVE%
%BcdEditPath% -set %BootGuid% OSDEVICE PARTITION=%SYSTEMDRIVE%
%BcdEditPath% -set %BootGuid% SYSTEMROOT \Windows
%BcdEditPath% -set %BootGuid% PATH \Windows\system32\%WinLoadPatchedName%
%BcdEditPath% -set %BootGuid% KERNEL %NtoskrnlPatchedName%
%BcdEditPath% -set %BootGuid% RECOVERYENABLED 0
%BcdEditPath% -set %BootGuid% NX OptIn
%BcdEditPath% -set %BootGuid% NOINTEGRITYCHECKS 1
%BcdEditPath% -set %BootGuid% INHERIT {bootloadersettings}
%BcdEditPath% -displayorder %BootGuid% -ADDLAST
%BcdEditPath% -timeout 30


ECHO   * Changing Service Startup Type.
sc config peauth start= demand >NUL


ECHO   * Done.
ECHO   * Please Restart your PC and select the Patched Boot Entry.

goto EXIT


:FILES_NOT_FOUND

ECHO   Failure: Patch files not found.
ECHO     "%OriginNtoskrnlPatchedPath%"
ECHO     "%OriginWinLoadPatchedPath%"

goto EXIT


:INVALID_VERSION

ECHO   Failure: Your Operating System Version is not supported.
ECHO            Make sure your Operating System is fully updated using Windows Update.
ECHO.
ECHO            We support the following versions:
ECHO              - Windows 7   [Version 6.1.7601]
ECHO              - Windows 8.1 [Version 6.3.9600]
ECHO              - Windows 10  [Version 10.0.10586]
ECHO.
ECHO            You are currently using:
ver

goto EXIT



:INVALID_ARCHITECTURE

ECHO   Failure: You must use a 64bit Operating System of Windows Vista, Windows 7, Windows 8.1 or Windows 10.

goto EXIT


:INVALID_BOOT

ECHO   Failure: Unable to determin the BIOS/UEFI System

goto EXIT


:EXIT

ECHO.
ECHO.

pause