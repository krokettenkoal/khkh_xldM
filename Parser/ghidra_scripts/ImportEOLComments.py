#TODO write a description for this script
#@author 
#@category Data
#@keybinding 
#@menupath 
#@toolbar 

f = askFile("Give me a file to open", "Go baby go!")

for line in file(f.absolutePath):  # note, cannot use open(), since that is in GhidraScript
    pieces = line.split("|")
    address = toAddr(long(pieces[0], 16))
    setEOLComment(address, pieces[1])
