
extern "C" void __declspec(dllexport) __stdcall MySSE_VMx16(float *pf) {
	__asm mov ecx,[pf]
	__asm movups xmm0,[ecx+  0] // vec0
	__asm movups xmm1,[ecx+ 16] // vec1
	__asm movups xmm2,[ecx+ 32] // vec2
	__asm movups xmm3,[ecx+ 48] // vec3
	__asm movups xmm4,[ecx+ 64] // mul0
	__asm movups xmm5,[ecx+ 80] // mul1
	__asm movups xmm6,[ecx+ 96] // mul2
	__asm movups xmm7,[ecx+112] // mul3
	__asm mulps xmm0, xmm4
	__asm mulps xmm1, xmm5
	__asm mulps xmm2, xmm6
	__asm mulps xmm3, xmm7
	__asm addps xmm0, xmm1
	__asm addps xmm0, xmm2
	__asm addps xmm0, xmm3
	__asm movups [ecx+ 0],xmm0
}
