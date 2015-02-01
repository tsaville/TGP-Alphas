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
		private static SpriteUV 	_sprite;
		private static TextureInfo	_textureInfo;
		private static Vector2		_min, _max;
		private static Bounds2		_box;
		private static float 		_scale, _angle, _rotationSpeed;
		private static int 			_frameTime, _animationDelay,
									_noOnSpritesheetWidth,
									_noOnSpritesheetHeight,
									_widthCount, _heightCount,
									_speed;
		
		//Public functions.
		public Player (Scene scene)
		{
			//Initialise Variables
			_frameTime = 0;
			_animationDelay = 4;
			_widthCount = 0;
			_heightCount = 0;
			_speed = 3;
			_scale = 0.75f;
			_angle = 0.0f;
			_rotationSpeed = 0.1f;
			
			//SpriteSheet Info
			_textureInfo  = new TextureInfo("/Application/textures/stick.png");
			_noOnSpritesheetWidth = 4;
			_noOnSpritesheetHeight = 2;
			
			//Create Sprite
			_sprite	 			= new SpriteUV();
			_sprite 			= new SpriteUV(_textureInfo);
			_sprite.UV.S 		= new Vector2(1.0f/_noOnSpritesheetWidth,1.0f/_noOnSpritesheetHeight);
			_sprite.Quad.S 		= new Vector2(_textureInfo.TextureSizef.X/_noOnSpritesheetWidth, _textureInfo.TextureSizef.Y/_noOnSpritesheetHeight);
			_sprite.Scale		= new Vector2(_scale, _scale);
			_sprite.CenterSprite();
			_sprite.Position 	= new Vector2(0.0f, ((_textureInfo.TextureSizef.Y/_noOnSpritesheetHeight)*_scale)*0.5f);
			
			
			//Add to the current scene.
			scene.AddChild(_sprite);
		}
		
		public void Dispose()
		{
			_textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{
			//Player Animation
			if(_frameTime == _animationDelay)
			{
				if (_widthCount == _noOnSpritesheetWidth)
				{
					_heightCount++;
					_widthCount = 0;
				}
				
				if (_heightCount == _noOnSpritesheetHeight)
				{
					//_widthCount++;
					_heightCount = 0;
				}
				
				_sprite.UV.T = new Vector2((1.0f/_noOnSpritesheetWidth)*_widthCount,(1.0f/_noOnSpritesheetHeight)*_heightCount);
				
				_widthCount++;
				//_heightCount++;
				_frameTime = 0;
			}
			
			_frameTime++;
			
			//Move Player
			_sprite.Position = new Vector2(_sprite.Position.X + _speed, _sprite.Position.Y);
			
			while (_sprite.Angle != _angle)
				_sprite.Rotate(_rotationSpeed);
			
			//Storing Bounds2 box data for collisions
			_min.X			= _sprite.Position.X;
			_min.Y			= _sprite.Position.Y;
			_max.X			= _sprite.Position.X + ((_textureInfo.TextureSizef.X/_noOnSpritesheetWidth)*_scale);
			_max.Y			= _sprite.Position.Y + ((_textureInfo.TextureSizef.Y/_noOnSpritesheetHeight)*_scale);
			_box.Min 		= _min;			
			_box.Max 		= _max;
		}
		
		//Set the height of the player
		public void SetYPos(int y) { _sprite.Position = new Vector2(_sprite.Position.X, y); }
		
		//Get and set the size of the player
		public void SetScale(float scale)
		{ 
			_scale = scale;
			_sprite.Scale = new Vector2(_scale, _scale);
			_sprite.Position = new Vector2(_sprite.Position.X, ((_textureInfo.TextureSizef.Y/_noOnSpritesheetHeight)*_scale)*0.5f);
		}
		public float GetScale() { return _scale; }
		
		//Get and set the rotation of the player
		public void SetRotation(float angle, float rotationSpeed) { _angle = angle; _rotationSpeed = rotationSpeed; }
		public Vector2 GetRotation(){ return new Vector2(_angle, _rotationSpeed); }
		
		//Get and set the speed of the player
		public void SetSpeed(int speed) { _speed = speed; }
		public int GetSpeed(){ return _speed; }
		
		//Get the position of the player
		public Vector2 GetPos() { return _sprite.Position; }
		
		//Get the collision box of the player
		public Bounds2 GetBox() { return _box; }
	}
}

