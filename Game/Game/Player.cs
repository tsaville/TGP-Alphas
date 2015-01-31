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
		private static float 		scale;
		private static int 			frameTime, animationDelay,
									noOnSpritesheetWidth,
									noOnSpritesheetHeight,
									widthCount,	heightCount,
									speed;
		
		//Accessors.
		//public SpriteUV Sprite { get{return sprite;} }
		
		//Public functions.
		public Player (Scene scene)
		{
			//Initialise Variables
			frameTime = 0;
			animationDelay = 4;
			widthCount = 0;
			heightCount = 0;
			speed = 3;
			scale = 0.75f;
			
			//SpriteSheet Info
			textureInfo  = new TextureInfo("/Application/textures/stick.png");
			noOnSpritesheetWidth = 4;
			noOnSpritesheetHeight = 2;
			
			//Create Sprite
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);
			sprite.UV.S = new Vector2(1.0f/noOnSpritesheetWidth,1.0f/noOnSpritesheetHeight);
			sprite.Quad.S 	= new Vector2(textureInfo.TextureSizef.X/noOnSpritesheetWidth, textureInfo.TextureSizef.Y/noOnSpritesheetHeight);
			sprite.Scale = new Vector2(scale, scale);
			sprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width / 5, 0.0f);
			
			//Add to the current scene.
			scene.AddChild(sprite);
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
			
			//Move Player
			sprite.Position = new Vector2(sprite.Position.X + speed, sprite.Position.Y);
			
			//Storing Bounds2 box data for collisions
			min.X			= sprite.Position.X;
			min.Y			= sprite.Position.Y;
			max.X			= sprite.Position.X + ((textureInfo.TextureSizef.X/noOnSpritesheetWidth)*scale);
			max.Y			= sprite.Position.Y + ((textureInfo.TextureSizef.Y/noOnSpritesheetHeight)*scale);
			box.Min 		= min;			
			box.Max 		= max;
		}
		
		//Set the height of the player
		public void SetYPos(int y) { sprite.Position = new Vector2(sprite.Position.X, y); }
		
		//Get and set the size of the player
		public void SetScale(float s) { sprite.Scale = new Vector2(s, s); }
		public float GetScale() { return scale; }
		
		//Get and set the speed of the player
		public void SetSpeed(int s) { speed = s; }
		public int GetSpeed(){ return speed; }
		
		//Get the position of the player
		public Vector2 GetPos() { return sprite.Position; }
		
		//Get the collision box of the player
		public Bounds2 GetBox() { return box; }
	}
}

