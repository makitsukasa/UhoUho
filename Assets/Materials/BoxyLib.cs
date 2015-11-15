using UnityEngine;
using System.Collections.Generic;

//2015.11.11

namespace BoxyLib
{

	public enum DropType { Void = 0, GO, RI, RA, BANANA1, BANANA2, BANANA3 };
	public enum DropColor {  Void = 0, Red, Green, Blue };

	public static class TouchUtil
	{
		//http://qiita.com/tempura/items/4a5482ff6247ec8873df

		public static bool GetTouch_Bool()
		{
			switch( GetTouch() )
			{
				case TouchUtil.TouchInfo.Began:
				case TouchUtil.TouchInfo.Moved:
				case TouchUtil.TouchInfo.Stationary:
					return true;

				default:
					return false;
			}
		}
		
		//タッチ情報を取得(エディタと実機を考慮)
		public static TouchInfo GetTouch()
		{
			if( Application.platform == RuntimePlatform.WindowsEditor )
			{
				if( Input.GetMouseButtonDown( 0 ) ) return TouchInfo.Began;
				if( Input.GetMouseButton	( 0 ) )	return TouchInfo.Stationary;
				if( Input.GetMouseButtonUp	( 0 ) ) return TouchInfo.Ended;
				return TouchInfo.None;
			}
			else
			{
				if( Input.touchCount > 0 )
				{
					return (TouchInfo)( (int)Input.GetTouch( 0 ).phase );
				}
				return TouchInfo.None;
			}
		}
		
		//タッチポジションを取得(エディタと実機を考慮)
		public static Vector3 GetTouchPosition()
		{
			if( Application.platform == RuntimePlatform.WindowsEditor )
			{
				TouchInfo touch = TouchUtil.GetTouch();
				if( touch != TouchInfo.None ) return Input.mousePosition;
				return Vector3.zero;
			}
			else
			{
				if( Input.touchCount > 0 )
				{
					Vector2 touchPos = Input.GetTouch(0).position;
					return new Vector3( touchPos.x, touchPos.y, 0 );
				}
				return Vector3.zero;
			}
		}
		
		public static Vector3 GetTouchWorldPosition( Camera camera )
		{
			return camera.ScreenToWorldPoint( GetTouchPosition() );
		}

		//タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
		public enum TouchInfo
		{
			// タッチなし
			None = -1,
			// 以下は UnityEngine.TouchPhase の値に対応
			Began = 0, Moved = 1, Stationary = 2, Ended = 3, Canceled = 4,
		}

	}


	public struct Vector2I
	{

		public int x, y;
		public Vector2I( int x, int y )
		{
			this.x = x;
			this.y = y;
		}
		public Vector2I( float x, float y )
		{
			this.x = (int)x;
			this.y = (int)y;
		}
		public Vector2I( Vector2F a )
		{
			this.x = (int)a.x;
			this.y = (int)a.y;
		}
		public Vector2I( Vector2 a )
		{
			this.x = (int)a.x;
			this.y = (int)a.y;
		}
		public Vector2I( Vector3 a )
		{
			this.x = (int)a.x;
			this.y = (int)a.y;
		}

		public override bool Equals( object obj )
		{
			if( obj is Vector2I )
			{
				return this == (Vector2I)obj;
			}
			return false;

		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "(" + this.x + "," + this.y + ")";
		}

		public static bool operator ==( Vector2I a, Vector2I b )
		{
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator !=( Vector2I a, Vector2I b )
		{
			return !( a == b );
		}

		public static bool operator <( Vector2I a, Vector2I b )
		{
			if( a.x == b.x ) return a.y < b.y;
			return a.x < b.x;
		}

		public static bool operator >( Vector2I a, Vector2I b )
		{
			if( a.x == b.x ) return a.y > b.y;
			return a.x > b.x;
		}

		public static Vector2I operator +( Vector2I a, Vector2I b )
		{
			return new Vector2I( a.x + b.x, a.y + b.y );
		}

		public static Vector2I operator -( Vector2I a, Vector2I b )
		{
			return new Vector2I( a.x - b.x, a.y - b.y );
		}

		public static Vector2I operator *( Vector2I a, int b )
		{
			return new Vector2I( a.x * b, a.y * b );
		}

		public static Vector2I operator /( Vector2I a, int b )
		{
			return new Vector2I( a.x / b, a.y / b );
		}

		public static void Swap( ref Vector2I a, ref Vector2I b )
		{
			Vector2I hoge = a;
			a = b;
			b = hoge;
		}

		public int dot( Vector2I v )
		{
			return this.x * v.x + this.y * v.y;
		}

		public int cross( Vector2I v )
		{
			return this.x * v.y - this.y * v.x;
		}

		public int LengthSq( Vector2I b )
		{
			return ( this.x - b.x ) * ( this.x - b.x ) + ( this.y - b.y ) * ( this.y - b.y );
		}

		public int LengthSq()
		{
			return this.LengthSq( new Vector2I( 0, 0 ) );
		}

		public bool IsParallel( Vector2I b )
		{
			return this.cross( b ) == 0;
		}

		public bool IsVertical( Vector2I b )
		{
			return this.dot( b ) == 0;
		}

		public bool IsAcute( Vector2I b )
		{
			return this.cross( b ) > 0;
		}

		public bool IsObtuse( Vector2I b )
		{
			return this.cross( b ) < 0;
		}

		public bool IsInCircle( Vector2I centerPos, float radius )
		{
			return IsInCircle( new Vector2F( centerPos.x, centerPos.y ), radius );
		}

		public bool IsInCircle( Vector2 centerPos, float radius )
		{
			return IsInCircle( new Vector2F( centerPos.x, centerPos.y ), radius );
		}

		public bool IsInCircle( Vector2F centerPos, float radius )
		{
			return centerPos.IsInCircle( this, radius );
		}

	}

	public struct Vector2F
	{

		public float x, y;
		public Vector2F( float x, float y )
		{
			this.x = x;
			this.y = y;
		}
		public Vector2F( Vector2I a )
		{
			this.x = (float)a.x;
			this.y = (float)a.y;
		}
		public Vector2F( Vector2 a )
		{
			this.x = (float)a.x;
			this.y = (float)a.y;
		}
		public Vector2F( Vector3 a )
		{
			this.x = (float)a.x;
			this.y = (float)a.y;
		}

		public override bool Equals( object obj )
		{
			if( obj is Vector2I )
			{
				return this == (Vector2F)obj;
			}
			return false;

		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "(" + this.x + "," + this.y + ")";
		}

		public static bool operator ==( Vector2F a, Vector2F b )
		{
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator !=( Vector2F a, Vector2F b )
		{
			return !( a == b );
		}

		public static bool operator <( Vector2F a, Vector2F b )
		{
			if( a.x == b.x ) return a.y < b.y;
			return a.x < b.x;
		}

		public static bool operator >( Vector2F a, Vector2F b )
		{
			if( a.x == b.x ) return a.y > b.y;
			return a.x > b.x;
		}

		public static void Swap( ref Vector2F a, ref Vector2F b )
		{
			Vector2F hoge = a;
			a = b;
			b = hoge;
		}

		public static Vector2F operator +( Vector2F a, Vector2F b )
		{
			return new Vector2F( a.x + b.x, a.y + b.y );
		}

		public static Vector2F operator -( Vector2F a, Vector2F b )
		{
			return new Vector2F( a.x - b.x, a.y - b.y );
		}

		public static Vector2F operator *( Vector2F a, float b )
		{
			return new Vector2F( a.x * b, a.y * b );
		}

		public static Vector2F operator /( Vector2F a, float b )
		{
			return new Vector2F( a.x / b, a.y / b );
		}

		public float LengthSq()
		{
			return x * x + y * y;
		}

		public float LengthSq( Vector2F b )
		{
			return ( this.x - b.x ) * ( this.x - b.x ) + ( this.y - b.y ) * ( this.y - b.y );
		}

		public float LengthSq( Vector2 b )
		{
			return ( this.x - b.x ) * ( this.x - b.x ) + ( this.y - b.y ) * ( this.y - b.y );
		}

		public float dot( Vector2F v )
		{
			return this.x * v.x + this.y * v.y;
		}

		public float cross( Vector2F v )
		{
			return this.x * v.y - this.y * v.x;
		}

		public bool IsParallel( Vector2F b )
		{
			return this.cross( b ) <= 0.0001 && this.cross( b ) >= -0.0001;
		}

		public bool IsVertical( Vector2F b )
		{
			return this.dot( b ) <= 0.0001 && this.dot( b ) >= -0.0001;
		}

		public bool IsAcute( Vector2F b )
		{
			return this.cross( b ) > 0;
		}

		public bool IsObtuse( Vector2F b )
		{
			return this.cross( b ) < 0;
		}

		public bool IsInCircle( Vector2F centerPos, float radius )
		{
			return ( this.LengthSq( centerPos ) < radius * radius );
		}

		public bool IsInCircle( Vector2 centerPos, float radius )
		{
			return IsInCircle( new Vector2F( centerPos.x, centerPos.y ), radius );
		}

		public bool IsInCircle( Vector2I a, float radius )
		{
			return IsInCircle( new Vector2F( a.x, a.y ), radius );
		}


	}


}

