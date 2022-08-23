# khkh_xldM
Fork of [kenjiuno](https://gitlab.com/kenjiuno)'s [KH2 MDLX tools](https://gitlab.com/kenjiuno/khkh_xldM) including various fixes and features.

## Introduction
The main purpose of this fork is to provide a working library for exporting `ASET` animation files from `MDLX`/`MSET` file pairs. This is done by porting the original (fixed) `khkh_xldMii_revel8n` tool into a .NET library, removing all Windows Forms dependencies. However, at the moment, this dependency is not fully removed, but simply outsourced. A Windows Forms App is still needed to call methods from this library, as SlimDX requires a ControlHandle reference to work. This restriction is planned to be eliminated as soon as possible. Check out the included project `khkh_xldMii_slim` for reference and usage.

## MdlxConvert
The main functionality of this library is provided by the `MdlxConvert` class. For easy and straightforward usage, call the static methods exposed by the class. The following list describes the most important methods in a brief manner:

### bool MdlxConvert.ToAset(string mdlxPath, IntPtr handle, Action<ExportState, ExportStatus> onProgress)
Converts the MDLX file at the specifies path to an ASET file. The method requires a corresponding MSET file in the same directory as the MDLX file.

#### Parameters
- *string mdlxPath*: The path of the MDLX file to convert
- *IntPtr handle*: A reference to ControlHandle calling the method (required by SlimDX). This will be removed in future versions for better cross-platform support.
- *Action<ExportState, ExportStatus> onProgress*: Callback function for the export progress. The function is called at several states of the export process containing the current state/status information.

#### Returns
True if the conversion has been successful.