import Blender

# Blender script
# for 2.49b
#  with Python 2.6.2

# Import instruction:
# * Launch Blender 2.49b
# * In Blender, type Shift+F11, then open then Script Window
# * Type Alt+O or [Text]menu -> [Open], then select and open mesh.py
# * Type Alt+P or [Text]menu -> [Run Python Script] to run the script!
# * Use Ctrl+LeftArrow, Ctrl+RightArrow to change window layout.

# IMPORT3.1 @ 11:45 2010/01/01
#           @ 15:40 2010/04/06
#       3.2 @ 21:26 2010/04/08
#       3.3 @ 1:08 2010/04/17
#           @ 5:00 2010/05/01
#   KH2>3.4 @ 21:46 2011/03/15
#           @ 20:50 2011/03/17
#           @ 23:18 2011/03/24
#           @ 17:15 2011/08/16
#           @ 20:47 2011/08/25

# Blender-python API: http://www.blender.org/documentation/249PythonDoc/index.html

scene = Blender.Scene.GetCurrent()

class MyMesh:
	# Works great with Blender 2.49b on Python 2.6.2

	yaMesh = None
	yaOb = None

	def PrepareMesh(self, name):
		self.yaMesh = Blender.Mesh.New(name)
		self.yaMesh.vertexColors = True
	
	def AddCoords(self, coords):
		self.yaMesh.verts.extend(coords)
	
	def AddFaces(self, faces):
		for f in faces:
			self.yaMesh.faces.extend([f])

	def AddColorUvMatFaces(self, faces, facecolors, faceuvs, faceMatImgs):
		facecnt = len(self.yaMesh.faces)
		
		for f in faces:
			self.yaMesh.faces.extend([f])

		self.yaMesh.faceUV = True
		
		facei = facecnt
	
		for facecol in facecolors:
			verti = 0
			
			for vertcol in facecol:
				yaCol = self.yaMesh.faces[facei].col[verti]
				yaCol.a = vertcol[0]
				yaCol.r = vertcol[1]
				yaCol.g = vertcol[2]
				yaCol.b = vertcol[3]
				
				verti = verti + 1
			
			facei = facei + 1

		if faceuvs != None:
			facei = facecnt
			for faceuv in faceuvs:
				aluv = []
				
				for vertuv in faceuv:
					aluv.append(Blender.Mathutils.Vector(vertuv[0], vertuv[1]))
				
				yaface = self.yaMesh.faces[facei]
				yaface.uv = aluv
					
				facei = facei + 1

		if faceMatImgs != None:
			facei = facecnt
			for faceMatImg in faceMatImgs:
				if faceMatImg != None:
					yaface = self.yaMesh.faces[facei]
					yaface.mat = faceMatImg[0]
					yaface.image = faceMatImg[1]

				facei = facei + 1

	def AddMat(self, mat):
		self.yaMesh.materials += [mat]	

	def MeshToOb(self, name):
		self.yaOb = scene.objects.new(self.yaMesh, name)
	
	def SetLocation(self, x, y, z):
		self.yaOb.setLocation(x, y, z)
		#self.yaMesh.update()
		#self.yaOb.makeDisplayList()
		
	def SetVertGr2(self, parentBoneName, factor, alverti):
		self.yaMesh.addVertGroup(parentBoneName)
		self.yaMesh.assignVertsToGroup(parentBoneName, alverti, factor, Blender.Mesh.AssignModes.ADD)

	def SetObRot(self, alBlk):
		ipo = Blender.Ipo.New('Object', 'ObIpo')
		rotx = ipo.addCurve('RotX')
		roty = ipo.addCurve('RotY')
		rotz = ipo.addCurve('RotZ')
		for alLine in alBlk:
			rotx.append((alLine[0], alLine[1]))
			roty.append((alLine[0], alLine[2]))
			rotz.append((alLine[0], alLine[3]))
		self.yaOb.setIpo(ipo)

class MyBone:
	armObj = None
	armData = None
	armAction = None

	def SetScale(self, x, y, z):
		self.armObj.setSize(x, y, z)

	def Prepare(self, scene, num):
		self.armObj = Blender.Object.New('Armature', 'Am' + str(num))
		self.armData = Blender.Armature.Armature('Arm' + str(num))
		self.armData.drawAxes = True
		self.armObj.link(self.armData)
		scene.link(self.armObj)
		self.armData.makeEditable()

	def AddBone(self, newBoneName, parentBoneName, x0, y0, z0, x1, y1, z1):
		b = Blender.Armature.Editbone()
		b.head = Blender.Mathutils.Vector(x0, y0, z0)
		b.tail = Blender.Mathutils.Vector(x1, y1, z1)
		
		if parentBoneName != None:
			b.parent = self.armData.bones[parentBoneName]
		
		self.armData.bones[newBoneName] = b

	def PrepareEnd(self):
		self.armData.update()
		self.armObj.makeDisplayList()
		Blender.Window.RedrawAll()

	def Edit(self):
		self.armData.makeEditable()

	def EndEdit(self):
		self.PrepareEnd()

	def AddChildWithArmature(self, yaOb):
		modifier = yaOb.modifiers.append(Blender.Modifier.Types.ARMATURE)
		modifier[Blender.Modifier.Settings.OBJECT] = self.armObj
		self.armObj.makeParent([yaOb], 1)
		#self.armObj.makeDisplayList()

	def AddChildWithCopyLoc(self, yaOb, parentBoneName):
		const = yaOb.constraints.append(Blender.Constraint.Type.COPYLOC)
		const[Blender.Constraint.Settings.TARGET] = self.armObj
		const[Blender.Constraint.Settings.BONE] = parentBoneName
		self.armObj.makeParent([yaOb], 1)
		#self.armObj.makeDisplayList()

	def SetPoseRot(self, actionName, alBlk): 
		self.armObj.action = Blender.Armature.NLA.NewAction(actionName)
		pose = self.armObj.getPose()
		dict = {}
		for kv in pose.bones.items():
			dict[kv[0]] = kv[1]
		
		for al in alBlk:
			frame = 1 + al['frame']
			for cols in al['joints']:
				b = dict[cols['b']]
				b.quat = Blender.Mathutils.Quaternion(cols['qw'], cols['qx'], cols['qy'], cols['qz'])
				b.insertKey(self.armObj, frame, Blender.Object.Pose.ROT)


class MyMats:
	yaMats = []
	yaImages = []

	def AddImage(self, texname, matname, filepath, alphaBlend):
		img = Blender.Image.Load(filepath)
		tex = Blender.Texture.New(texname)
		tex.image = img
		mat = Blender.Material.New(matname)
		mat.setTexture(0, tex, Blender.Texture.TexCo.UV, Blender.Texture.MapTo.COL)
		#mat.setMode('Shadeless')
		mat.setMode('VColLight')
		if alphaBlend == True:
			mat.mode |= Blender.Material.Modes.ZTRANSP
			mat.textures[0].blendmode = Blender.Texture.BlendModes.MULTIPLY
			mat.textures[0].mapto |= Blender.Texture.MapTo.ALPHA
		self.yaMats += [mat]
		self.yaImages += [img]

	def GetMat(self, i):
		return self.yaMats[i]

	def GetImage(self, i):
		return self.yaImages[i]

class BondUt:
	@staticmethod
	def BondArm(parentArm, parentBoneNames, childArm, childBoneNames, cnt, srt):
		childBones = childArm.getPose().bones
		
		for i in range(cnt):
			if 't' in srt:
				cb = childBones[childBoneNames[i]]
				const = cb.constraints.append(Blender.Constraint.Type.COPYLOC)
				const[Blender.Constraint.Settings.COPY] = Blender.Constraint.Settings.COPYX|Blender.Constraint.Settings.COPYY|Blender.Constraint.Settings.COPYZ
				const[Blender.Constraint.Settings.TARGET] = parentArm
				const[Blender.Constraint.Settings.BONE] = parentBoneNames[i]
				#const[Blender.Constraint.Settings.OWNERSPACE] = Blender.Constraint.Settings.SPACE_POSE
				#const[Blender.Constraint.Settings.TARGETSPACE] = [Blender.Constraint.Settings.SPACE_POSE]

			if 'r' in srt:
				const = cb.constraints.append(Blender.Constraint.Type.COPYROT)
				const[Blender.Constraint.Settings.COPY] = Blender.Constraint.Settings.COPYX|Blender.Constraint.Settings.COPYY|Blender.Constraint.Settings.COPYZ
				const[Blender.Constraint.Settings.TARGET] = parentArm
				const[Blender.Constraint.Settings.BONE] = parentBoneNames[i]
				#const[Blender.Constraint.Settings.OWNERSPACE] = Blender.Constraint.Settings.SPACE_POSE
				#const[Blender.Constraint.Settings.TARGETSPACE] = [Blender.Constraint.Settings.SPACE_POSE]

			if 's' in srt:
				const = cb.constraints.append(Blender.Constraint.Type.COPYSIZE)
				const[Blender.Constraint.Settings.COPY] = Blender.Constraint.Settings.COPYX|Blender.Constraint.Settings.COPYY|Blender.Constraint.Settings.COPYZ
				const[Blender.Constraint.Settings.TARGET] = parentArm
				const[Blender.Constraint.Settings.BONE] = parentBoneNames[i]
				#const[Blender.Constraint.Settings.OWNERSPACE] = Blender.Constraint.Settings.SPACE_POSE
				#const[Blender.Constraint.Settings.TARGETSPACE] = [Blender.Constraint.Settings.SPACE_POSE]

class Aniut:
	@staticmethod
	def SetPoseSRTv(actionName, bone, alBlk): 
		bone.armObj.action = Blender.Armature.NLA.NewAction(actionName)
		
		bones = {}

		pose = bone.armObj.getPose()
		for kv in pose.bones.items():
			bones[kv[0]] = kv[1]

		for al in alBlk:
			frame = int(al['frame'])
			for joint in al['joints']:
				b = bones[joint['b']]

				mybone = bone

				if 'tv' in joint:
					tv = joint['tv']
					b.loc = Blender.Mathutils.Vector(tv[0], tv[1], tv[2])
					b.insertKey(mybone.armObj, frame, Blender.Object.Pose.LOC)

				if 'qv' in joint:
					qv = joint['qv']
					b.quat = Blender.Mathutils.Quaternion(qv[3], qv[0], qv[1], qv[2])
					b.insertKey(mybone.armObj, frame, Blender.Object.Pose.ROT)

				if 'sv' in joint:
					sv = joint['sv']
					b.size = Blender.Mathutils.Vector(sv[0], sv[1], sv[2])
					b.insertKey(mybone.armObj, frame, Blender.Object.Pose.SIZE)

if 0:
	mat = MyMats()
	mat.AddImage('tex0', 'mat0', 'C:\\Windows\\Web\\Wallpaper\\Characters\\img19.jpg', True)
	ya = MyMesh()
	ya.PrepareMesh('g-001')
	ya.AddCoords(
		[
			[ 0, 0, 0], #vert0
			[ 1, 0, 0], #vert1
			[ 0, 1, 0], #vert2
			[ 1, 1, 0]  #vert3
		])
	ya.AddMat(mat.GetMat(0))
	ya.AddColorUvMatFaces(
		[
			[0, 1, 2], #face0 tri
			[2, 1, 3]  #face1 tri
		],[
			[[255,255,0,  0],[255,0,255,  0],[  0,  0,  0,255]], #face0 clr
			[[255,255,0,255],[255,0,255,255],[255,255,255,  0]]  #face1 clr
		],[
			[[0,0],[1,0],[0,1]], #face0 uv
			[[0,1],[1,0],[1,1]]  #face1 uv
		],[
			[0, mat.GetImage(0)], #face0 MatImg
			[0, mat.GetImage(0)]  #face1 MatImg
		])
	ya.MeshToOb('g')
