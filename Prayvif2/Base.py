import Blender

# IMPORT3.1 @ 11:45 2010/01/01

scene = Blender.Scene.GetCurrent()

class MyMesh:
	# Works great with Blender 2.49a on Python 2.6.2

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
