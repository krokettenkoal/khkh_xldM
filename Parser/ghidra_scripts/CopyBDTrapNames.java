//CopyBDTrapNames from EBOOT.ELF
//@author 
//@category Data
//@keybinding 
//@menupath 
//@toolbar 

import ghidra.app.script.GhidraScript;
import ghidra.program.model.address.Address;
import ghidra.program.model.address.AddressFactory;
import ghidra.program.model.address.AddressSpace;
import ghidra.program.model.address.GenericAddress;
import ghidra.program.model.listing.FunctionManager;
import ghidra.program.model.mem.MemoryAccessException;
import ghidra.program.model.symbol.Symbol;
import ghidra.program.model.symbol.SymbolTable;

public class CopyBDTrapNames extends GhidraScript {

	@Override
	protected void run() throws Exception {
		AddressSpace ram = currentProgram.getAddressFactory().getAddressSpace("ram");
		SymbolTable st = currentProgram.getSymbolTable();

		int num = askInt("ask", "num BD_TRAP?");
		println("# You are at: " + String.format("%X", currentAddress.getOffset()));
		for (int x = 0; x < num; x++) {
			String name = "";
			int ptr1 = getIntBigEndian(currentAddress.getOffset() + 8 * x);
			if (ptr1 != 0) {
				int ptr2 = getIntBigEndian(ptr1);
				if (ptr2 != 0) {
					Symbol[] s = st.getSymbols(ram.getAddress(ptr2));
					for (Symbol symbol : s) {
						name = symbol.getName();
						break;
					}
				}
			}
			println(String.format("%d,%s", x, name));
		}
	}

	protected int getIntBigEndian(long addr) throws MemoryAccessException {
		byte[] bytes = new byte[4];
		Address a = currentProgram.getAddressFactory().getAddressSpace("ram").getAddress(addr);
		currentProgram.getMemory().getBytes(a, bytes);
		int val = (bytes[0] & 255) * 0x1000000 + (bytes[1] & 255) * 0x10000 + (bytes[2] & 255) * 0x100
				+ (bytes[3] & 255);
		return val;
	}

}
