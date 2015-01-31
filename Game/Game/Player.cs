using System;


using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class Player
	{
		//Private variables.
		private static SpriteUV 	sprite;
		private static TextureInfo	textureInfo;
		private static Vector2		min, max;
		private static Bounds2		box;
		private static int 			frameTime, animationDelay,
									noOnSpritesheetWidth,
									noOnSpritesheetHeight,
									widthCount,	heightCount;
		
		//Accessors.
		//public SpriteUV Sprite { get{return sprite;} }
		
		//Public functions.
		public Player (Scene scene)
		{
			//SpriteSheet Info
			textureInfo  = new TextureInfo("/Application/textures/stick.png");
			noOnSpritesheetWidth = 4;
			noOnSpritesheetHeight = 2;
			
			//Create Sprite
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);
			sprite.UV.S = new Vector2(1.0f/noOnSpritesheetWidth,1.0f/noOnSpritesheetHeight);
			sprite.Quad.S 	= new Vector2(textureInfo.TextureSizef.X/noOnSpritesheetWidth, textureInfo.TextureSizef.Y/noOnSpritesheetHeight);
			//sprite.Scale = new Vector2(0.5f,0.5f);
			sprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width / 5, 0.0f);
			
			//Add to the current scene.
			scene.AddChild(sprite);
			
			//Initialise Variables
			frameTime = 0;
			animationDelay = 4;
			widthCount = 0;
			heightCount = 0;
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
			//Player Animation
			if(frameTime == animationDelay)
			{
				if (widthCount == noOnSpritesheetWidth)
				{
					heightCount++;
					widthCount = 0;
				}
				
				if (heightCount == noOnSpritesheetHeight)
				{
					//widthCount++;
					heightCount = 0;
				}
				
				sprite.UV.T = new Vector2((1.0f/noOnSpritesheetWidth)*widthCount,(1.0f/noOnSpritesheetHeight)*heightCount);
				
				widthCount++;
				//heightCount++;
				frameTime = 0;
			}
			
			frameTime++;
			
			sprite.Position = new Vector2(sprite.Position.X+3,0.0f);

			
			//Storing Bounds2 box data for collisions
			min.X			= sprite.Position.X;
			min.Y			= sprite.Position.Y;
			max.X			= sprite.Position.X + (textureInfo.TextureSizef.X);
			max.Y			= sprite.Position.Y + (textureInfo.TextureSizef.Y);
			box.Min 		= min;			
			box.Max 		= max;
		}
		
		public void SetYPos(int y)
		{
			sprite.Position = new Vector2(sprite.Position.X, y);
		}
		
		public Vector2 GetPos()
		{
			return sprite.Position;
		}
		
		public Bounds2 GetBox()
		{	
			return box;
		}
	}
}
