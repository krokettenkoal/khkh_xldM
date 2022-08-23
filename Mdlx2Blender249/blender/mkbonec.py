import Blender

print "---"

scene = Blender.Scene.GetCurrent

a = Blender.Armature.Get("Armature.002")
am = Blender.Object.Get("AM")
a.makeEditable()

eb = Blender.Armature.Editbone()
eb.head = Blender.Mathutils.Vector(1,0,0)
eb.tail = Blender.Mathutils.Vector(2,0,0)
eb.parent = a.bones["BR"]
a.bones["ABC8"] = eb
a.update()
am.makeDisplayList()
Blender.Window.RedrawAll()

for b in a.bones.items():
	print b
