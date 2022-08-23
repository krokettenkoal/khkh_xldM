import Blender

print "IMPORTER2"

armObj = Blender.Object.New('Armature', 'AM')
armData = Blender.Armature.Armature('MDLX_ARM')
armData.drawAxes = True
armObj.link(armData)
scene = Blender.Scene.GetCurrent()
scene.link(armObj)
armData.makeEditable()

b = Blender.Armature.Editbone()
b.head = Blender.Mathutils.Vector(0,0,0)
b.tail = Blender.Mathutils.Vector(1,1,0)
armData.bones["root"] = b

b = Blender.Armature.Editbone()
b.parent = armData.bones["root"]
b.head = Blender.Mathutils.Vector(0,2,0)
b.tail = Blender.Mathutils.Vector(1,3,0)
armData.bones["b"] = b

armData.update()
armObj.makeDisplayList()
scene.update()
Blender.Window.RedrawAll()
