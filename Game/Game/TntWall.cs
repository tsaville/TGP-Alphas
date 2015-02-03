using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class TntWall
	{
		private static SpriteUV 	boxSprite;
		private static TextureInfo	boxTextureInfo;
		private static SpriteUV 	pluSprite;
		private static TextureInfo	pluTextureInfo;
		private static SpriteUV 	rockSprite;
		private static TextureInfo	rockTextureInfo;
		private static SpriteUV 	exploSprite;
		private static TextureInfo	exploTextureInfo;
		private static SpriteUV 	dynoSprite;
		private static TextureInfo	dynoTextureInfo;
		private static float		yPos;
		private static float		xPos;
		private static Scene tScene;
		private static bool			blown = false;
		private static int 			counter = 20;
		
		public TntWall (Scene scene)
		{
			boxTextureInfo = new TextureInfo("/Application/textures/tntbox.png");
			pluTextureInfo = new TextureInfo("/Application/textures/tntplun.png");
			rockTextureInfo = new TextureInfo("/Application/textures/rock.png");
			exploTextureInfo = new TextureInfo("/Application/textures/explo.png");
			dynoTextureInfo = new TextureInfo("/Application/textures/dyno.png");
			
			tScene = scene;
			
			boxSprite	 	= new SpriteUV(boxTextureInfo);
			pluSprite 		= new SpriteUV(pluTextureInfo);	
			rockSprite 		= new SpriteUV(rockTextureInfo);	
			exploSprite 	= new SpriteUV(exploTextureInfo);
			dynoSprite 	= new SpriteUV(dynoTextureInfo);
			
			boxSprite.Quad.S 	= boxTextureInfo.TextureSizef;
			boxSprite.Position = new Vector2(200.0f,20.0f);
			
			dynoSprite.Quad.S 	= boxTextureInfo.TextureSizef;
			dynoSprite.Position = new Vector2(320.0f,18.0f);
			
			pluSprite.Quad.S 	= pluTextureInfo.TextureSizef;
			pluSprite.Position = new Vector2(200.0f,84.0f);
			
			rockSprite.Quad.S 	= rockTextureInfo.TextureSizef;
			rockSprite.Position = new Vector2(400.0f,20.0f);
			
			exploSprite.Quad.S 	= exploTextureInfo.TextureSizef;
			exploSprite.Position = new Vector2(rockSprite.Position.X - 150.0f, rockSprite.Position.Y);
			
			scene.AddChild(rockSprite);
			scene.AddChild(pluSprite);
			scene.AddChild(boxSprite);
			scene.AddChild(dynoSprite);
			
		}
				
		public void Dispose()
		{
			boxTextureInfo.Dispose();
			pluTextureInfo.Dispose();
			rockTextureInfo.Dispose ();
			exploTextureInfo.Dispose ();
		}
		
		public void Update(float deltaTime, float x, float y)
		{			
			
			yPos = (y * (Director.Instance.GL.Context.GetViewport().Height / 2))
					+ (Director.Instance.GL.Context.GetViewport().Height / 2)
					- (pluTextureInfo.TextureSizef.Y / 2);
			
			xPos = (x * (Director.Instance.GL.Context.GetViewport().Width / 2))
					+ (Director.Instance.GL.Context.GetViewport().Width / 2)
					- (pluTextureInfo.TextureSizef.Y / 2);
			
			if(yPos <= pluSprite.Position.Y + 64 && yPos >= pluSprite.Position.Y - 64 
			   && xPos <= pluSprite.Position.X + 32 && xPos >= pluSprite.Position.X - 32)
				
				{
					if(yPos < 84.0f && yPos > 24.0f)
						pluSprite.Position = new Vector2(200.0f , yPos);	
				}	
			if(pluSprite.Position.Y <= 30.0f)
				blowUpRock ();
			
			counter--;
			if(counter<0)
				tScene.RemoveChild(exploSprite, true);
		}	
		
		public void blowUpRock()
		{
			tScene.RemoveChild(rockSprite, true);
			//tScene.RemoveChild(boxSprite, true);
			tScene.RemoveChild(pluSprite, true);
			tScene.RemoveChild(dynoSprite, true);
			if(!blown)
			{
				tScene.AddChild(exploSprite);
				blown = true;
				counter = 20;
			}
			
			
		}
		
		public void CheckCollision()
		{
			
		}
		
		public void Tapped()
		{
			
		}
	}
}

