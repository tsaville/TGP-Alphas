using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace Game
{
	public class Background
	{
		//Private variables.
		//volcano
		private SpriteUV 	volcSprite;
		private TextureInfo	volcTextureInfo;
		//walls
		private SpriteUV	wallSprite;
		private SpriteUV	wallSprite2;
		private SpriteUV	entrSprite;
		private TextureInfo	wallTextureInfo;
		private TextureInfo	wall2TextureInfo;
		//smog
		private SpriteUV 	smogSprite;
		private SpriteUV	smogSprite2;
		
		private SpriteUV	smog2Sprite;
		private SpriteUV	smog2Sprite2;
		
		private TextureInfo	smogTextureInfo;
		private TextureInfo	smog2TextureInfo;
		
		private float		width;
		private float		height2;
		
		private static Scene tScene;
		
		//Public functions.
		public Background (Scene scene)
		{
			initVolcano ();
			initwalls ();
			initSmogs ();
			
			tScene = scene;
			//Get sprite bounds.
			Bounds2 b = volcSprite.Quad.Bounds2();
			width     = b.Point10.X;
			Bounds2 b2 = volcSprite.Quad.Bounds2();
			height2     = b2.Point11.Y;
			
			//Position background.
			volcSprite.Position = new Vector2(0.0f, 0.0f);
			smogSprite.Position = new Vector2(0.0f, 0.0f);
			smogSprite2.Position = new Vector2(width-2.0f, 0.0f);
			
			wallSprite.Position = new Vector2(width*2, 0.0f);
			wallSprite2.Position = new Vector2(width*3-2.0f, 0.0f);
					
			entrSprite.Position = new Vector2(width, 0.0f);
			
			addToScene(scene);


		}	
		
		public void initVolcano()
		{
			//The basic background
			volcSprite	= new SpriteUV();			
			volcTextureInfo  	= new TextureInfo("/Application/textures/volc2.png");
			volcSprite 			= new SpriteUV(volcTextureInfo);
			volcSprite.Quad.S 	= volcTextureInfo.TextureSizef;
		}
		
		public void initSmogs()
		{
			//Create the scrolling smog sprites
			smogSprite			= new SpriteUV();
			smogSprite2 		= new SpriteUV();
			
			smog2Sprite			= new SpriteUV();
			smog2Sprite2 		= new SpriteUV();
			
			//smog textures
			smogTextureInfo  	= new TextureInfo("/Application/textures/dclouds2.png");
			smog2TextureInfo  	= new TextureInfo("/Application/textures/dclouds2.png");
			
			smogSprite 			= new SpriteUV(smogTextureInfo);
			smogSprite.Quad.S 	= smogTextureInfo.TextureSizef;
			
			smogSprite2 		= new SpriteUV(smogTextureInfo);
			smogSprite2.Quad.S 	= smogTextureInfo.TextureSizef;
			
		}
		
		public void initwalls()
		{
			//Create the scrolling wall sprites
			wallSprite	= new SpriteUV();
			wallSprite2 = new SpriteUV();
			
			//wall textures
			wallTextureInfo  = new TextureInfo("/Application/textures/brownwall.png");
			wall2TextureInfo = new TextureInfo("/Application/textures/cavestart.png");
			
			wallSprite = new SpriteUV(wallTextureInfo);
			wallSprite2 = new SpriteUV(wallTextureInfo);
			entrSprite = new SpriteUV(wall2TextureInfo);
			wallSprite.Quad.S 	= wallTextureInfo.TextureSizef;
			wallSprite2.Quad.S 	= wallTextureInfo.TextureSizef;
			entrSprite.Quad.S 	= wall2TextureInfo.TextureSizef;
		}
		
		public void addToScene(Scene scene)
		{
			//Add to the current scene.
			scene.AddChild(volcSprite);
			scene.AddChild (smogSprite);
			scene.AddChild (smogSprite2);
			scene.AddChild (wallSprite);
			scene.AddChild (wallSprite2);
			scene.AddChild (entrSprite);
		}
		
		public void Dispose()
		{
			volcTextureInfo.Dispose();
			smogTextureInfo.Dispose();
			wallTextureInfo.Dispose ();
			wall2TextureInfo.Dispose ();
			smog2TextureInfo.Dispose ();
		}
		
		public void Update(float deltaTime)
		{	
			UpdateWalls(deltaTime);	
			UpdateSmog(deltaTime);
		}
		
		public void UpdateSmog(float deltaTime)
		{
			//Moves the smog overlay so that it's a constant scrolling image
			smogSprite.Position = new Vector2(smogSprite.Position.X-0.4f, smogSprite.Position.Y);
			smogSprite2.Position = new Vector2(smogSprite2.Position.X-0.4f, smogSprite2.Position.Y);
			
			//Resets the position once off screen
			if(smogSprite.Position.X <= -width+5.0f)
				smogSprite.Position = new Vector2(width-2.0f, 0.0f);
			if(smogSprite2.Position.X <= -width+5.0f)
				smogSprite2.Position = new Vector2(width-2.0f, 0.0f);
				
		}
		
		public void UpdateWalls(float deltaTime)
		{
			//Moves the wall overlay so that it's a constant scrolling image
			wallSprite.Position = new Vector2(wallSprite.Position.X-1.5f, wallSprite.Position.Y);
			wallSprite2.Position = new Vector2(wallSprite2.Position.X-1.5f, wallSprite2.Position.Y);
			entrSprite.Position = new Vector2(entrSprite.Position.X-1.5f, entrSprite.Position.Y);
			//Resets the position once off screen
			if(wallSprite.Position.X <= -width+2.0f)
				wallSprite.Position = new Vector2(width-2.0f, 0.0f);
			if(wallSprite2.Position.X <= -width+2.0f)
				wallSprite2.Position = new Vector2(width-2.0f, 0.0f);
			if(entrSprite.Position.X < -width)
				tScene.RemoveChild (entrSprite, true);
			
		}
	}
}


