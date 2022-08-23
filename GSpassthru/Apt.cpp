
#include <stdio.h>
#include <windows.h>
#include <tchar.h>

#define DLLEXP __declspec(dllexport)

typedef __int32 s32;
typedef unsigned __int8 u8;
typedef unsigned __int32 u32;
typedef unsigned __int64 u64;
typedef struct { int temp; } keyEvent;
typedef struct { int temp; } freezeData;

typedef s32  (CALLBACK* _GSinit)();
typedef void (CALLBACK* _GSshutdown)();
typedef s32  (CALLBACK* _GSopen)(void *pDsp, char *Title, int multithread);
typedef void (CALLBACK* _GSclose)();
typedef void (CALLBACK* _GSgifTransfer1)(u32 *pMem, u32 addr);
typedef void (CALLBACK* _GSgifTransfer2)(u32 *pMem, u32 size);
typedef void (CALLBACK* _GSgifTransfer3)(u32 *pMem, u32 size);
typedef void (CALLBACK* _GSreadFIFO)(u64 *pMem);
typedef void (CALLBACK* _GSvsync)(int field);
typedef void (CALLBACK* _GSmakeSnapshot)(char *path);
typedef void (CALLBACK* _GSkeyEvent)(keyEvent* ev);
typedef s32  (CALLBACK* _GSfreeze)(int mode, freezeData *data);
typedef void (CALLBACK* _GSconfigure)();
typedef s32  (CALLBACK* _GStest)();
typedef void (CALLBACK* _GSabout)();
typedef void (CALLBACK* _GSreadFIFO2)(u64 *pMem, int qwc);
typedef void (CALLBACK* _GSirqCallback)(void (*callback)());
typedef void (CALLBACK* _GSsetBaseMem)(void*);
typedef void (CALLBACK* _GSwriteCSR)(u32 value);
typedef void (CALLBACK* _GSchangeSaveState)(int, const char* filename);
typedef void (CALLBACK* _GSreset)();
typedef void (CALLBACK* _GSgifSoftReset)(u32 mask);
typedef void (CALLBACK* _GSsetFrameSkip)(int frameskip);
typedef void (CALLBACK* _GSsetGameCRC)(int);

typedef u32  (CALLBACK* _PS2EgetLibType)(void);
typedef u32  (CALLBACK* _PS2EgetLibVersion2)(u32 type);
typedef char*(CALLBACK* _PS2EgetLibName)(void);

_GSinit pfn_GSinit = NULL;
_GSshutdown pfn_GSshutdown = NULL;
_GSopen pfn_GSopen = NULL;
_GSclose pfn_GSclose = NULL;
_GSgifTransfer1 pfn_GSgifTransfer1 = NULL;
_GSgifTransfer2 pfn_GSgifTransfer2 = NULL;
_GSgifTransfer3 pfn_GSgifTransfer3 = NULL;
_GSreadFIFO pfn_GSreadFIFO = NULL;
_GSvsync pfn_GSvsync = NULL;
_GSmakeSnapshot pfn_GSmakeSnapshot = NULL;
_GSkeyEvent pfn_GSkeyEvent = NULL;
_GSfreeze pfn_GSfreeze = NULL;
_GSconfigure pfn_GSconfigure = NULL;
_GStest pfn_GStest = NULL;
_GSabout pfn_GSabout = NULL;
_GSreadFIFO2 pfn_GSreadFIFO2 = NULL;
_GSirqCallback pfn_GSirqCallback = NULL;
_GSsetBaseMem pfn_GSsetBaseMem = NULL;
_GSwriteCSR pfn_GSwriteCSR = NULL;
_GSchangeSaveState pfn_GSchangeSaveState = NULL;
_GSreset pfn_GSreset = NULL;
_GSgifSoftReset pfn_GSgifSoftReset = NULL;
_GSsetFrameSkip pfn_GSsetFrameSkip = NULL;
_GSsetGameCRC pfn_GSsetGameCRC = NULL;

_PS2EgetLibType pfn_PS2EgetLibType = NULL;
_PS2EgetLibVersion2 pfn_PS2EgetLibVersion2 = NULL;
_PS2EgetLibName pfn_PS2EgetLibName = NULL;

bool iserr = false;
HMODULE hLib = NULL;

bool core() {
	if (!iserr) {
		if (hLib != NULL)
			return true;
#if 0
		if (hLib == NULL) hLib = LoadLibrary(_T("ZeroGS KOSMOS 0.96 sse2.dll"));
		if (hLib == NULL) hLib = LoadLibrary(_T("plugins\\ZeroGS KOSMOS 0.96 sse2.dll"));
#else
		if (hLib == NULL) hLib = LoadLibrary(_T("ZeroGS KOSMOS 0.96 non sse2.dll"));
		if (hLib == NULL) hLib = LoadLibrary(_T("plugins\\ZeroGS KOSMOS 0.96 non sse2.dll"));
#endif
		if (hLib != NULL) {
			if (true
#define LOADF(X) && (pfn_##X = (_##X)GetProcAddress(hLib, #X)) != NULL
				LOADF(PS2EgetLibType)
				LOADF(PS2EgetLibVersion2)
				LOADF(PS2EgetLibName)
				LOADF(GSinit)
				LOADF(GSshutdown)
				LOADF(GSopen)
				LOADF(GSclose)
				LOADF(GSgifTransfer1)
				LOADF(GSgifTransfer2)
				LOADF(GSgifTransfer3)
				LOADF(GSreadFIFO)
				LOADF(GSvsync)
				LOADF(GSmakeSnapshot)
				LOADF(GSkeyEvent)
				LOADF(GSfreeze)
				LOADF(GSconfigure)
				LOADF(GStest)
				LOADF(GSabout)
				LOADF(GSreadFIFO2)
				LOADF(GSirqCallback)
				LOADF(GSsetBaseMem)
				LOADF(GSwriteCSR)
				LOADF(GSchangeSaveState)
				LOADF(GSreset)
				LOADF(GSgifSoftReset)
				LOADF(GSsetFrameSkip)
				LOADF(GSsetGameCRC)
			) {
				return true;
			}
		}
	}
	iserr = true;
	return false;
}

// *--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*
// |  helper
// *

class Writer {
public:
	FILE *f;

	Writer() {
		f = NULL;
	}
	~Writer() {
		Close();
	}
	void Close() {
		if (f != NULL)
			fclose(f), f = NULL;
	}
	void Open() {
		Close();
		fopen_s(&f, "W:\\GSpassthru.bin", "wb");  
	}
	void Queue(u32 tag, const void *psz, u32 cch) {
		if (f != NULL) {
			fwrite(&tag, 4, 1, f);
			fwrite(&cch, 4, 1, f);
			//fflush(f);
			fwrite(psz, cch, 1, f);
		}
	}
};
Writer wr;

#define PMODE ((u64 *)(pBasePS2Mem+0x0000))		// 1
#define SMODE1 ((u64 *)(pBasePS2Mem+0x0010))	// 2
#define SMODE2 ((u64 *)(pBasePS2Mem+0x0020))	// 4
// SRFSH
#define SYNCH1 ((u64 *)(pBasePS2Mem+0x0040))	// 16
#define SYNCH2 ((u64 *)(pBasePS2Mem+0x0050))	// 32
#define SYNCV ((u64 *)(pBasePS2Mem+0x0060))		// 64
#define DISPFB1 ((u64 *)(pBasePS2Mem+0x0070))	// 128
#define DISPLAY1 ((u64 *)(pBasePS2Mem+0x0080))	// 256
#define DISPFB2 ((u64 *)(pBasePS2Mem+0x0090))	// 512
#define DISPLAY2 ((u64 *)(pBasePS2Mem+0x00a0))	// 1024
#define EXTBUF ((u64 *)(pBasePS2Mem+0x00b0))	// 2048
#define EXTDATA ((u64 *)(pBasePS2Mem+0x00c0))	// 4096
#define EXTWRITE ((u64 *)(pBasePS2Mem+0x00d0))	// 8192
#define BGCOLOR ((u64 *)(pBasePS2Mem+0x00e0))	// 16384
#define CSR ((u64 *)(pBasePS2Mem+0x1000))		// 32768
#define IMR ((u64 *)(pBasePS2Mem+0x1010))		// 65536
#define BUSDIR ((u64 *)(pBasePS2Mem+0x1040))	// 131072
#define SIGLBLID ((u64 *)(pBasePS2Mem+0x1080))	// 262144

class PrivREGSet {
public:
	u8* pBasePS2Mem;
	u64 v[19];
	struct {
		u32 mask;
		u64 v[19];
	} vrec;

	PrivREGSet() {
		pBasePS2Mem = NULL;
		ZeroMemory(v, sizeof(v));
		ZeroMemory(&vrec, sizeof(vrec));
	}
	void Test() {
		if (pBasePS2Mem != NULL) {
			int pos = 0;
			vrec.mask = 0;
#define REC_VAL(X,I) if (v[I] != *(X)) { vrec.v[pos++] = v[I] = *(X), vrec.mask |= (1 << I); }
			REC_VAL(PMODE,0);
			REC_VAL(SMODE1,1);
			REC_VAL(SMODE2,2);

			REC_VAL(SYNCH1,4);
			REC_VAL(SYNCH2,5);
			REC_VAL(SYNCV,6);
			REC_VAL(DISPFB1,7);
			REC_VAL(DISPLAY1,8);
			REC_VAL(DISPFB2,9);
			REC_VAL(DISPLAY2,10);
			REC_VAL(EXTBUF,11);
			REC_VAL(EXTDATA,12);
			REC_VAL(EXTWRITE,13);
			REC_VAL(BGCOLOR,14);
			REC_VAL(CSR,15);
			REC_VAL(IMR,16);
			REC_VAL(BUSDIR,17);
			REC_VAL(SIGLBLID,18);
			wr.Queue(41, &vrec, 4 +8*pos);
		}
	}
};
PrivREGSet prs;

u32 calcSizeGIFtagz(u32 *pMem, u32 addr) {
	u8 cureop = 0;
	u32 qwc = 0;
	while (true) {
		u32 *data = ((u32 *)(((u8 *)pMem) + (addr & 0x3fff)));
		u32 v0 = data[0];
		u32 v1 = data[1];
		u32 nloop = (v0 & 0x7FFF);
		u8 eop = (u8)((v0 >> 15) & 1);
		u8 nreg = (u8)((v1 >> 28) & 0xF);
		u8 flg = (u8)((v1 >> 26) & 0x3);
		addr += 16; qwc++;
		if (cureop != 0 || nloop == 0) break;
		cureop = eop;

		u32 n;
		switch (flg) {
			case 0: // PACKED
				{
					n = nloop * nreg;
					break;
				}
			case 1: // REG
				{
					n = (nloop * nreg +1) / 2;
					break;
				}
			case 2: // IMAGE
			default:
				{
					n = nloop;
					break;
				}
		}
		if (n == 0)
			break;
		addr += 16 * n; qwc += n;
	}
	return 16 * qwc;
}

// *--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*
// |  GSxxx funct
// *

extern "C" s32 DLLEXP CALLBACK GSinit() {
	s32 r = pfn_GSinit();
	return r;
}
extern "C" void DLLEXP CALLBACK GSshutdown() {
	pfn_GSshutdown();
}
extern "C" s32 DLLEXP CALLBACK GSopen(void *pDsp, char *Title, int multithread) {
	s32 r = pfn_GSopen(pDsp, Title, multithread);
	wr.Open();
	return r;
}
extern "C" void DLLEXP CALLBACK GSclose() {
	pfn_GSclose();
	wr.Close();
}
extern "C" void DLLEXP CALLBACK GSgifTransfer1(u32 *pMem, u32 addr) {
	wr.Queue(1, (u8 *)pMem +(addr&0x3ff0), calcSizeGIFtagz(pMem, addr));

	pfn_GSgifTransfer1(pMem, addr);
}
extern "C" void DLLEXP CALLBACK GSgifTransfer2(u32 *pMem, u32 size) {
	wr.Queue(2, (u8 *)pMem, size*16);

	pfn_GSgifTransfer2(pMem, size);
}
extern "C" void DLLEXP CALLBACK GSgifTransfer3(u32 *pMem, u32 size) {
	wr.Queue(3, (u8 *)pMem, size*16);

	pfn_GSgifTransfer3(pMem, size);
}
extern "C" void DLLEXP CALLBACK GSreadFIFO(u64 *pMem) {
	wr.Queue(11, pMem, 8*1);

	pfn_GSreadFIFO(pMem);
}
extern "C" void DLLEXP CALLBACK GSvsync(int field) {
	prs.Test();
	wr.Queue(21, &field, 4);

	pfn_GSvsync(field);
}
extern "C" void DLLEXP CALLBACK GSmakeSnapshot(char *path) {
	pfn_GSmakeSnapshot(path);
}
extern "C" void DLLEXP CALLBACK GSkeyEvent(keyEvent* ev) {
	pfn_GSkeyEvent(ev);
}
extern "C" s32 DLLEXP CALLBACK GSfreeze(int mode, freezeData *data) {
	s32 r = pfn_GSfreeze(mode, data);
	return r;
}
extern "C" void DLLEXP CALLBACK GSconfigure() {
	pfn_GSconfigure();
}
extern "C" s32 DLLEXP CALLBACK GStest() {
	s32 r = pfn_GStest();
	return r;
}
extern "C" void DLLEXP CALLBACK GSabout() {
	pfn_GSabout();
}
extern "C" void DLLEXP CALLBACK GSreadFIFO2(u64 *pMem, int qwc) {
	wr.Queue(12, pMem, 8*qwc);

	pfn_GSreadFIFO2(pMem, qwc);
}
extern "C" void DLLEXP CALLBACK GSirqCallback(void (*callback)()) {
	pfn_GSirqCallback(callback);
}
extern "C" void DLLEXP CALLBACK GSsetBaseMem(void *pMem) {
	prs.pBasePS2Mem = (u8 *)pMem;

	pfn_GSsetBaseMem(pMem);
}
extern "C" void DLLEXP CALLBACK GSwriteCSR(u32 value) {
	wr.Queue(31, &value, 4);

	pfn_GSwriteCSR(value);
}
extern "C" void DLLEXP CALLBACK GSchangeSaveState(int v, const char* filename) {
	pfn_GSchangeSaveState(v, filename);
}
extern "C" void DLLEXP CALLBACK GSreset() {
	pfn_GSreset();
}
extern "C" void DLLEXP CALLBACK GSgifSoftReset(u32 mask) {
	pfn_GSgifSoftReset(mask);
}
extern "C" void DLLEXP CALLBACK GSsetFrameSkip(int frameskip) {
	wr.Queue(32, &frameskip, 4);

	pfn_GSsetFrameSkip(frameskip);
}
extern "C" void DLLEXP CALLBACK GSsetGameCRC(int crc) {
	int aa[] = {crc};
	wr.Queue(33, aa, sizeof(aa));

	pfn_GSsetGameCRC(crc);
}

// *--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*
// |  PS2xxx funct
// *

extern "C" u32 DLLEXP CALLBACK PS2EgetLibType(void) {
	if (core()) {
		return pfn_PS2EgetLibType();
	}
	return 0;
}
extern "C" u32 DLLEXP CALLBACK PS2EgetLibVersion2(u32 type) {
	if (core()) {
#define PS2E_GS_VERSION   0x0006
		const unsigned __int8 version = PS2E_GS_VERSION;
		const unsigned __int8 revision = 0;
		const unsigned __int8 build = 0;
		const unsigned __int8 minor = 2;
		return (version<<16) | (revision<<8) | build | (minor << 24);
	}
	return 0;
}
extern "C" LPCSTR DLLEXP CALLBACK PS2EgetLibName(void) {
	if (core()) {
		return "GSpassthru ";
	}
	return "?";
}

BOOL WINAPI DllMain(
	HINSTANCE hinstDLL,
	DWORD fdwReason,
	LPVOID lpvReserved
) {
	if (fdwReason == DLL_PROCESS_ATTACH)
		return true;
	return false;
}

// 575,340,544 ˆÈ‰“
