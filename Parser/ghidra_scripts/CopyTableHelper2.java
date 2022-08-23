//TODO CopyReferenceSymbolNames
//@author 
//@category Data
//@keybinding 
//@menupath 
//@toolbar 

import java.nio.ByteBuffer;
import java.nio.charset.Charset;

import ghidra.app.script.GhidraScript;
import ghidra.app.services.ClipboardService;
import ghidra.program.model.address.Address;
import ghidra.program.model.address.AddressFactory;
import ghidra.program.model.address.AddressSpace;
import ghidra.program.model.address.GenericAddress;
import ghidra.program.model.listing.FunctionManager;
import ghidra.program.model.mem.Memory;
import ghidra.program.model.mem.MemoryAccessException;
import ghidra.program.model.symbol.Symbol;
import ghidra.program.model.symbol.SymbolTable;

public class CopyTableHelper2 extends GhidraScript {

	private AddressSpace ram;
	private Memory memory;

	@Override
	protected void run() throws Exception {
		ram = currentProgram.getAddressFactory().getAddressSpace("ram");
		memory = currentProgram.getMemory();

		println("# You are at: " + String.format("%X", currentAddress.getOffset()));
		int numItems = askInt("ask", "num items?");
		for (int x = 0; x < numItems; x++) {
			String text = "";
			int str = getIntLittleEndian(currentAddress.getOffset() + 4 * 18 * x + 0);
			String func = "";
			if (str != 0) {
				byte[] bytes = new byte[32];
				memory.getBytes(ram.getAddress(str), bytes);
				func = Charset.forName("ascii").decode(ByteBuffer.wrap(bytes)).toString().split("\0")[0];
			}
			text = String.format("%d,%s", x, func);
			println(text);
		}
	}

	protected int getIntBigEndian(long addr) throws MemoryAccessException {
		byte[] bytes = new byte[4];
		memory.getBytes(ram.getAddress(addr), bytes);
		int val = (bytes[0] & 255) * 0x1000000 + (bytes[1] & 255) * 0x10000 + (bytes[2] & 255) * 0x100
				+ (bytes[3] & 255);
		return val;
	}

	protected int getIntLittleEndian(long addr) throws MemoryAccessException {
		byte[] bytes = new byte[4];
		memory.getBytes(ram.getAddress(addr), bytes);
		int val = (bytes[3] & 255) * 0x1000000 + (bytes[2] & 255) * 0x10000 + (bytes[1] & 255) * 0x100
				+ (bytes[0] & 255);
		return val;
	}
}
