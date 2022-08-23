KH1 dumper:
- copy KINGDOM.IDX and make a ISO into the same folder as kh1_file_dumper.exe
- make a *.bat or run from commandline

Usage:


kh1_file_dumper.exe kh.iso KINGDOM.IDX [options]



options:

"-dN" = dump from "ard files" NameTable (like yaz0r's dumper)

"-dM" = dump all Music files (dat(bgm+wd),wd,vset,vsb)

"-dE" = dump all Event files (evm,dpp,dpx)
"-dO" = dump Other files (ard,evm,wdt,dat,dbt,wpn,se,mag)
"-dT" = dump all files from nametbl.txt






KH2 dumper:

- copy KH2.IDX & KH2.IMG to the same folder as kh2_file_dumper.exe or use it directly from DVD
- make a *.bat or run from commandline

Usage:


kh2_file_dumper.exe "Path to KH2.IDX & KH2.IMG" [options]

options:

"-dN" dump from 00objentry.bin Nametable (like yaz0r's dumper)

"-dT" dump from nametbl.txt


If you wanna use the included KH2.IDX and nametbl.txt, you need KH2FM! it will not work with other KH versions.
The reason for this is simple, KH2.IDX is 1 from 20 tables, the included KH2.IDX is modified to have all 20 tables in 1.