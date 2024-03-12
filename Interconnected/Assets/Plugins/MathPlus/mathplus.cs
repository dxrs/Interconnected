using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MathPlus {

	public class mathPlus {

		public static float Pi = 3.141593f;
		public static float Tau = 6.283185f;
		public static float Phi = 1.618034f;
		public static float E = 2.718281f;
		public static float[] PrecalculatedSquareRoots = new float[] {0, 1, 1.42f, 1.73f, 2, 2.24f, 2.45f, 2.65f, 2.83f, 3, 3.16f, 3.32f, 3.46f, 3.61f, 3.74f, 3.87f, 4};
		public static Color ColorBlack = new Color (0, 0, 0);
		public static Color ColorRed = new Color (1, 0, 0);
		public static Color ColorGreen = new Color (0, 1, 0);
		public static Color ColorBlue = new Color (0, 0, 1);
		public static Color ColorYellow = new Color (1, 1, 0);
		public static Color ColorLightBlue = new Color (0, 1, 1);
		public static Color ColorLightGreen = new Color (0.45f, 1, 0);
		public static Color ColorLightRed = new Color (1, 0.2f, 0.2f);
		public static Color ColorPink = new Color (1, 0, 1);
		public static Color ColorLightPink = new Color (1, 0.3f, 1);
		public static Color ColorOrange = new Color (1, 0.35f, 0);
		public static string[] AlphabetDictionary = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
		public static string[] CapitalAlphabetDictionary = new string[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
		public static string[] ZeroToNineDictionary = new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
		public static string[] AlphabetAndZeroToNineDictionary = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
		public static string[] AlphabetAndAndCapitalAlphabetAndZeroToNineDictionary = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

		public static int AverageIntArray (int[] Ints) { //Average an array of integers
			int total = 0;

			for (int i = 0; i < Ints.Length; i++) {
				total += Ints [i];
			}
			total = total / Ints.Length;

			return total;
		}

		public static int AddIntArray (int[] Ints) {
			int total = 0;

			for (int i = 0; i < Ints.Length; i++) {
				total += Ints [i];
			}

			return total;
		}

		public static float AverageFloatArray (float[] Ints) { //Average an array of floats
			float total = 0;

			for (int i = 0; i < Ints.Length; i++) {
				total += Ints [i];
			}
			total = total / Ints.Length;

			return total;
		}

		public static float AddFloatArray (float[] floats) {
			float total = 0;

			for (int i = 0; i < floats.Length; i++) {
				total += floats [i];
			}

			return total;
		}

		public static string GetStringChars (string String, int startChar, int endChar) {
			string final = "";

			for(int i = startChar; i < endChar; i++) {
				final += String.Substring (i, 1);
			}

			return final;
		}

		public static Color RandomColor () {
			Color final = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));

			final.r = final.r / 255;
			final.g = final.g / 255;
			final.b = final.b / 255;

			return final;
		}

		public static Color RandomColorWithAlpha () {
			Color final = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));

			final.r = final.r / 255;
			final.g = final.g / 255;
			final.b = final.b / 255;
			final.a = final.a / 255;

			return final;
		}

		public static Color RandomSmartColor (int vividness, int damping) {
			Color final = new Color(0, 0, 0);

			int r = Random.Range (0, 255);
			int g = Random.Range (0, 255);
			int b = Random.Range (0, 255);

			if (r > vividness) {
				g = g - damping;
				b = b - damping;
			}

			if (g > vividness) {
				r = r - damping;
				b = b - damping;
			}

			if (b > vividness) {
				r = r - damping;
				g = g - damping;
			}

			if (r < 0) {
				r = 0;
			}

			if (g < 0) {
				g = 0;
			}

			if (b < 0) {
				b = 0;
			}

			final.r = r;
			final.g = g;
			final.b = b;

			final.r = final.r / 255;
			final.g = final.g / 255;
			final.b = final.b / 255;

			return final;
		}

		public static int[] RandomIntArray(int min, int max, int count) {
			int[] final = new int[count];

			for (int i = 0; i < count; i++) {
				final[i] = Random.Range (min, max);
			}

			return final;
		}

		public static float[] RandomFloatArray(float min, float max, int count) {
			float[] final = new float[count];

			for (int i = 0; i < count; i++) {
				final[i] = Random.Range (min, max);
			}

			return final;
		}

		public static string RandomString (string[] Dictionary, int length) {
			string final = "";

			for (int i = 0; i < length; i++) {
				int num = Random.Range (0, Dictionary.Length);
				final += Dictionary [num];
			}

			return final;
		}

		public static string RandomStringUsingAlphabet (int length) {
			string final = "";

			for (int i = 0; i < length; i++) {
				int num = Random.Range (0, AlphabetDictionary.Length);
				final += AlphabetDictionary [num];
			}

			return final;
		}

		public static string RandomStringUsingNumberDictionary (int length) {
			string final = "";

			for (int i = 0; i < length; i++) {
				int num = Random.Range (0, ZeroToNineDictionary.Length);
				final += ZeroToNineDictionary [num];
			}

			return final;
		}

		public static string[] RandomStringArray(string[] Dictionary, int length, int count) {
			string[] final = new string[count];

			for (int c = 0; c < count; c++) {
				
				string current = "";

				for (int i = 0; i < length; i++) {
					int num = Random.Range (0, Dictionary.Length);
					current += Dictionary [num];
				}

				final [c] = current;
			}

			return final;
		}

		public static string[] RandomStringArrayUsingAlphebet(int length, int count) {
			string[] final = new string[count];

			for (int c = 0; c < count; c++) {

				string current = "";

				for (int i = 0; i < length; i++) {
					int num = Random.Range (0, AlphabetDictionary.Length);
					current += AlphabetDictionary [num];
				}

				final [c] = current;
			}

			return final;
		}

		public static string[] RandomStringArrayNumberDictionary(int length, int count) {
			string[] final = new string[count];

			for (int c = 0; c < count; c++) {

				string current = "";

				for (int i = 0; i < length; i++) {
					int num = Random.Range (0, ZeroToNineDictionary.Length);
					current += ZeroToNineDictionary [num];
				}

				final [c] = current;
			}

			return final;
		}

		public static string[] ReplaceInStringArray (string[] StringArray, string ToReplace, string Replacement) {
			string[] final = new string[StringArray.Length];

			for (int i = 0; i < StringArray.Length; i++) {
				string str = StringArray[i];

				str = str.Replace (ToReplace, Replacement);

				final [i] = str;
			}

			return final;
		}

		public static string RemoveCommasFromString (string String) {
			string final = String;
			final = String.Replace (",", "");
			return final;
		}

		public static string RemovePeriodsFromString (string String) {
			string final = String;
			final = String.Replace (".", "");
			return final;
		}

		public static int ParseIntSafe (string String) {
			int final = 0;

			String = RemoveCommasFromString (String);
			String = RemovePeriodsFromString (String);

			int.Parse (String);

			return final;
		}

		public static float TempretureConvertFtoC (float F) {
			float final = (F - 32) * 5/9;
			return final;
		}

		public static float TempretureConvertFtoK (float F) {
			float final = (F - 32) * 5/9 + 273.15f;
			return final;
		}

		public static float TempretureConvertCtoF (float C) {
			float final = (C * 9/5) + 32;
			return final;
		}

		public static float TempretureConvertCtoK (float C) {
			float final = C + 273.15f;
			return final;
		}

		public static float TempretureConvertKtoF (float K) {
			float final = (K - 273.15f) * 9/5 + 32;
			return final;
		}

		public static float TempretureConvertKtoC (float K) {
			float final = K - 273.15f;
			return final;
		}

		public static float PercentError(float a, float b) {
			float final = (a - b)/a;
			return final;
		}

		public static int[] CompareIntArrayDifference(int toCompare, int[] array) {
			int[] final = new int[array.Length];

			for (int i = 0; i < array.Length; i++) {
				final [i] = toCompare - array [i];
			}

			return final;
		}

		public static float[] CompareFloatArrayDifference(float toCompare, float[] array) {
			float[] final = new float[array.Length];

			for (int i = 0; i < array.Length; i++) {
				final [i] = toCompare - array [i];
			}

			return final;
		}

		public static Vector2 Vector2ParabolaPoint(Vector2 a, Vector2 b, Vector2 c, float t) {
			t = Mathf.Clamp01 (t);

			Vector2 q = (1-t)*a + t*b;
			Vector2 r = (1-t)*b + t*c;
			Vector2 p = (1-t)*q + t*r;

			return p;
		}

		public static Vector3 Vector3ParabolaPoint(Vector3 a, Vector3 b, Vector3 c, float t) {
			t = Mathf.Clamp01 (t);

			Vector3 q = (1-t)*a + t*b;
			Vector3 r = (1-t)*b + t*c;
			Vector3 p = (1-t)*q + t*r;

			return p;
		}

		public static Vector2[] Vector2Parabola(Vector2 a, Vector2 b, Vector2 c, int quality) {
			Vector2[] final = new Vector2[quality];

			float interval = 1 / (float)quality;
			float t = 0;

			for (int i = 0; i < quality; i++) {
				t += interval;

				Vector2 q = (1-t)*a + t*b;
				Vector2 r = (1-t)*b + t*c;
				Vector2 p = (1-t)*q + t*r;
				final [i] = p;
			}

			return final;
		}

		public static Vector3[] Vector3Parabola(Vector3 a, Vector3 b, Vector3 c, int quality) {
			Vector3[] final = new Vector3[quality];

			Vector3 q;
			Vector3 r;
			Vector3 p;

			float interval = 1 / (float)quality;
			float t = 0;

			for (int i = 0; i < quality; i++) {
				t += interval;

				q = (1-t)*a + t*b;
				r = (1-t)*b + t*c;
				p = (1-t)*q + t*r;
				final [i] = p;
			}

			return final;
		}

		public static int IntWeightedAverage (int a, int b, float t) {
			t = Mathf.Clamp01 (t);

			int final = (int)((1 - t)*a + t*b);
			return final;
		}

		public static float FloatWeightedAverage (float a, float b, float t) {
			t = Mathf.Clamp01 (t);

			float final = (1 - t)*a + t*b;
			return final;
		}

		public static float PythagoreanTheorem (float a, float b, float c) { //Leave the one 0 to be calculated
			float final = 0;

			if(c == 0) {
				float A = a * a;
				float B = b * b;
				final = Mathf.Sqrt ((A + B));
				return final;
			}
			if (a == 0) {
				float B = b * b;
				float C = c * c;
				final = Mathf.Sqrt((C - B));
				return final;
			}
			if (b == 0) {
				float A = a * a;
				float C = c * c;
				final = Mathf.Sqrt ((C - A));
				return final;
			}
			return 0;
		}

		public static float AreaOfRectangle (float l, float w) {
			float final = l * w;
			return final;
		}

		public static float AreaOfTrapezoid (float a, float b, float h) {
			float final = ((a + b) / 2) * h;
			return final;
		}

		public static float AreaOfCircle (float r) {
			float final = Pi * (r * r);
			return final;
		}

		public static float AreaOfTriangle (float b, float h) {
			float final = (1 / 2) * b * h;
			return final;
		}

		public static float VolumeOfRectangularprism (float l, float w, float h) {
			float final = l * w * h;
			return final;
		}

		public static float VolumeOfSphere (float r) {
			float final = (4 / 3) * Pi * (r * r);
			return final;
		}

		public static float VolumeOfCone (float BaseArea, float h) {
			float final = (1 / 3) * BaseArea * h;
			return final;
		}

		public static float VolumeOfCube (float l) {
			float final = l * l * l;
			return final;
		}

		public static float VolumeOfCylinder (float BaseArea, float h) {
			float final = BaseArea * h;
			return final;
		}

		public static float PerimeterOfSquare (float s) {
			float final = 4 * s;
			return final;
		}

		public static float PerimeterOfRectangle (float l, float w) {
			float final = (2 * w) + (2 * l);
			return final;
		}

		public static float PerimeterOfTriangle (float a, float b, float c) {
			float final = a + b + c;
			return final;
		}

		public static int[] SortIntArray(int[] Array) {
			int[] final = Array;
			for (int b = 0; b < final.Length; b++) {
				for (int i = 0; i < final.Length-1; i++) {
					if(final[i] > final[i+1]) {
						int a = final[i];
						final[i] = final[i+1];
						final[i+1] = a;
					}
				}
			}
			return final;
		}

		public static float[] SortFloatArray(float[] Array) {
			float[] final = Array;
			for (int b = 0; b < final.Length; b++) {
				for (int i = 0; i < final.Length-1; i++) {
					if(final[i] > final[i+1]) {
						float a = final[i];
						final[i] = final[i+1];
						final[i+1] = a;
					}
				}
			}
			return final;
		}

		public static int[] SortIntArrayReverse(int[] Array) {
			int[] final = Array;
			for (int b = 0; b < final.Length; b++) {
				for (int i = 0; i < final.Length-1; i++) {
					if(final[i] < final[i+1]) {
						int a = final[i];
						final[i] = final[i+1];
						final[i+1] = a;
					}
				}
			}
			return final;
		}

		public static float[] SortFloatArrayReverse(float[] Array) {
			float[] final = Array;
			for (int b = 0; b < final.Length; b++) {
				for (int i = 0; i < final.Length-1; i++) {
					if(final[i] < final[i+1]) {
						float a = final[i];
						final[i] = final[i+1];
						final[i+1] = a;
					}
				}
			}
			return final;
		}

		public static string[] SortStringArrayByLength(string[] String) {
			string[] final = String;
			for (int b = 0; b < String.Length; b++) {
				for (int i = 0; i < String.Length-1; i++) {
					if (final [i].Length > final [i + 1].Length) {
						string a = final [i];
						final [i] = final [i + 1];
						final [i + 1] = a;
					}
				}
			}
			return final;
		}

		public static string[] SortStringArrayByLengthReverse(string[] String) {
			string[] final = String;
			for (int b = 0; b < String.Length; b++) {
				for (int i = 0; i < String.Length-1; i++) {
					if (final [i].Length < final [i + 1].Length) {
						string a = final [i];
						final [i] = final [i + 1];
						final [i + 1] = a;
					}
				}
			}
			return final;
		}

		public static Vector3[] appendVector3Arrays (Vector3[] a, Vector3[] b) {
			Vector3[] final = new Vector3[a.Length + b.Length + 1];

			for (int i = 0; i < a.Length; i++) {
				final [i] = a [i];
			} 

			for (int i = 0; i < b.Length; i++) {
				final [i + a.Length] = b [i];
			}

			return final;
		}

		public static Vector2[] appendVector2Arrays (Vector2[] a, Vector2[] b) {
			Vector2[] final = new Vector2[a.Length + b.Length + 1];

			for (int i = 0; i < a.Length; i++) {
				final [i] = a [i];
			} 

			for (int i = 0; i < b.Length; i++) {
				final [i + a.Length] = b [i];
			}

			return final;
		}

		public static Vector3[] CreateCatmullSpline (Vector3[] points, int resolution, bool loop) {
			Vector3[] final = new Vector3[resolution * points.Length + 1];

			for (int i = 0; i < points.Length; i++) {
				if ((i == 0 || i == points.Length - 2 || i == points.Length - 1) && !loop) {
					continue;
				}
				Vector3[] a = DisplayCatmullRomSpline(i, points, resolution);
				final = processVector3array (final, a, Vector3.zero);
			}
			return final;
		}
		static Vector3[] DisplayCatmullRomSpline(int pos, Vector3[] points, int res) {
			Vector3[] outPos = new Vector3[res+5];

			Vector3 p0 = points[ClampListPos(pos - 1, points)];
			Vector3 p1 = points[pos];
			Vector3 p2 = points[ClampListPos(pos + 1, points)];
			Vector3 p3 = points[ClampListPos(pos + 2, points)];

			Vector3 lastPos = p1;
			p1 = lastPos;


			float resolution = (float)1/res;

			int loops = Mathf.FloorToInt(1f / resolution);

			for (int i = 1; i <= loops; i++) {
				float t = i * resolution;
				Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);
				outPos [i] = newPos;
				lastPos = newPos;
			}
			return outPos;
		}
		static int ClampListPos(int pos, Vector3[] points) {
			if (pos < 0) {
				pos = points.Length - 1;
			}
			if (pos > points.Length) {
				pos = 1;
			} else if (pos > points.Length - 1) {
				pos = 0;
			}
			return pos;
		}
		static Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
			Vector3 a = 2f * p1;
			Vector3 b = p2 - p0;
			Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
			Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;
			Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
			return pos;
		}
		static Vector3[] processVector3array (Vector3[] a, Vector3[] b, Vector3 cleanThis) {
			Vector3[] start = new Vector3[a.LongLength + b.LongLength];

			for (int i = 0; i < a.Length; i++) {
				start [i] = a [i];
			} 
			for (int i = 0; i < b.Length; i++) {
				start [i + a.Length] = b [i];
			}

			Vector3[] removal = new Vector3[start.Length];
			int count = 0;

			for (int i = 0; i < start.Length; i++) {
				if (start [i] != cleanThis) {
					removal [count] = start [i];
					count++;
				}
			}
			Vector3[] output = new Vector3[count];
			for (int i = 0; i < count; i++) {
				output [i] = removal [i];
			}
			return output;
		}



	}

}