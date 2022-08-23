# khkh_xldM
Fork of [kenjiuno](https://gitlab.com/kenjiuno)'s [KH2 MDLX tools](https://gitlab.com/kenjiuno/khkh_xldM) including various fixes and features.

## Introduction
The main purpose of this fork is to provide a working library for exporting `ASET` animation files from *MDLX*/*MSET* file pairs. This is done by porting the original (fixed) **[khkh_xldMii_revel8n](khkh_xldMii_revel8n)** tool into a .NET 6 library, removing all Windows Forms dependencies. However, at the moment, this dependency is not fully removed, but simply outsourced. A Windows Forms App is still needed to call methods from this library, as [SlimDX](https://github.com/mrvux/SlimDX) requires a `ControlHandle` reference to work. This restriction is planned to be eliminated as soon as possible. Check out the included project **[khkh_xldMii_slim](khkh_xldMii_slim)** for reference and usage.

## Installing/building
Currently, there are no pre-built releases for download as further testing needs to be done. The library ships with all dependencies, including SlimDX, so you should be able to build (working) projects yourself.

## MdlxConvert
The main functionality of this library is provided by the [`MdlxConvert`](mdlx2aset/MdlxConvert.cs) class. For easy and straightforward usage, call the static methods exposed by the class. The following list describes the most important methods in a brief manner:

### bool MdlxConvert.ToAset(string mdlxPath, Action<ExportState, ExportStatus> onProgress, IntPtr handle)
Converts the MDLX file at the specifies path to an ASET file. The method requires a corresponding MSET file in the same directory as the MDLX file.

#### Parameters
- **string mdlxPath**: The path of the MDLX file to convert
- **Action<ExportState, ExportStatus> onProgress**: Callback function for the export progress. The function is called at several states of the export process containing the current state/status information.
- **IntPtr handle** (*optional*): A reference to the handle (hWnd) of the calling window/process (required by SlimDX). Default: the current (invoking) process handle.

#### Returns
`True` if the conversion has been successful.