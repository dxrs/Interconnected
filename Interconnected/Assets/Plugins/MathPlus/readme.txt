//////////////////////////
---How do I use Math Plus?
//////////////////////////

1. At the top of your script add "using MathPlus;"
2. Then by typing "mathPlus." you can see all the functions if you are using monodevelop.
Otherwise please look down in this document for a list of all the functions and what they do
3. You are done!

/////////
---Demos:
/////////

- Parabola Demo:
Press play and select the TestParabola game object. There should be a script called Test.
To move the parabloa around adjust the a, b, and c variables. The quality variable tells
how many spheres to put in, making the curve look curvier at a loss of performance.

- Color Demo:
Press play and move the sliders to influance how smart random color changes the random color!
Then compare it against a completely random color

/////////////////////////
---What is this constant?
/////////////////////////

PrecalculatedSquareRoots = {0, 1, 1.42f, 1.73f, 2, 2.24f, 2.45f, 2.65f, 2.83f, 3, 3.16f, 3.32f, 3.46f, 3.61f, 3.74f, 3.87f, 4};
	These are some precalculated square roots, the index is the same as the square root


---These are best used with the random string and random string array functions:
AlphabetDictionary = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
CapitalAlphabetDictionary = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
ZeroToNineDictionary = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
AlphabetAndZeroToNineDictionary = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
AlphabetAndAndCapitalAlphabetAndZeroToNineDictionary = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

////////////////////////////////
-----What does this function do?
////////////////////////////////


mathPlus.AverageIntArray (int[] Ints)
	You give it an Int array and it averages all of the values in the array

mathPlus.AddIntArray (int[] Ints)
	It adds all the values in the int array you provide

mathPlus.AverageFloatArray (float[] Ints)
	Averages all the values in a float array

mathPlus.AddFloatArray (float[] floats)
	Add all the values in a float array

mathPlus.GetStringChars (string String, int startChar, int endChar)
	get characters in a string from one char to another 

mathPlus.RandomColor ()
	makes a totally random color

mathPlus.RandomColorWithAlpha ()
	makes a totally random color with a random alpha

mathPlus.RandomSmartColor (int vividness, int damping)
	makes a random color and damps the colors to make it more vivid

mathPlus.RandomIntArray(int min, int max, int count)
	make an array of random ints from min to max

mathPlus.RandomFloatArray(float min, float max, int count)
	make an array of random floats from a min to a max

mathPlus.RandomString (string[] Dictionary, int length)
	make a random string using a dictionary as a set of characters to pick from

mathPlus.RandomStringUsingAlphabet (int length)
	make a random string using the alphabet dictionary

mathPlus.RandomStringUsingNumberDictionary (int length)
	make a random string using a the number dictionary

mathPlus.RandomStringArray(string[] Dictionary, int length, int count)
	make an array of strings using a dictionary.

mathPlus.RandomStringArrayUsingAlphebet(int length, int count)
	make an array of strings using a the alphabet dictionary

mathPlus.RandomStringArrayNumberDictionary(int length, int count)
	make an array of strings using the number dictionary

mathPlus.ReplaceInStringArray (string[] StringArray, string ToReplace, string Replacement)
	replace chars in every value of a string array

mathPlus.RemoveCommasFromString (string String)
	remove commas from a string

mathPlus.RemovePeriodsFromString (string String)
	remove periods from a string

mathPlus.ParseIntSafe (string String)
	parse an int but removing commas/periods first to prevent an error

mathPlus.TempretureConvertFtoC (float F)
	convert F to C

mathPlus.TempretureConvertFtoK (float F)
	convert F to K

mathPlus.TempretureConvertCtoF (float C)
	convert C to F

mathPlus.TempretureConvertCtoK (float C)
	convert C to K

mathPlus.TempretureConvertKtoF (float K)
	convert K to f

mathPlus.TempretureConvertKtoC (float K)
	convert K to C

mathPlus.PercentError(float a, float b)
	Calculate percent of error given a and b

mathPlus.CompareIntArrayDifference(int toCompare, int[] array)
	compares every value in an array to an int. Then it returns the difference as an array

mathPlus.CompareFloatArrayDifference(float toCompare, float[] array)
	compares every value in an array to an float. Then it returns the difference as an array

mathPlus.Vector2ParabolaPoint(Vector2 a, Vector2 b, Vector2 c, float t)
	generates a point along a parabola given 3 handles to guide the arc and a time(0, 1)

mathPlus.Vector3ParabolaPoint(Vector3 a, Vector3 b, Vector3 c, float t)
	generates a point along a parabola given 3 handles to guide the arc and a time(0, 1)

mathPlus.Vector2Parabola(Vector2 a, Vector2 b, Vector2 c, int quality)
	creates a 2D parabola and returns an array with points along the parabolic arc

mathPlus.Vector3Parabola(Vector3 a, Vector3 b, Vector3 c, int quality)
	creates a 3D parabola and returns an array with points along the parabolic arc

mathPlus.IntWeightedAverage (int a, int b, float t)
	weighted averages 2 ints and returns an int

mathPlus.FloatWeightedAverage (float a, float b, float t)
	weighted averages 2 floats and returns a float

mathPlus.PythagoreanTheorem (float a, float b, float c)
	to calculate c, leave C = 0 and supply A and B.
	to calculate b, leave B = 0 and supply A and C.
	to calculate a, leave A = 0 and supply C and B.

mathPlus.AreaOfRectangle (float l, float w)
	calcualte the area of a rectangle

mathPlus.AreaOfTrapezoid (float a, float b, float h)
	calculate the area of a trapezoid, a and b are the lengths of the bases and h is the height.

mathPlus.AreaOfCircle (float r)
	calculate the area of a circle

mathPlus.AreaOfTriangle (float b, float h)
	calculate the area of a RIGHT triangle

mathPlus.VolumeOfRectangularprism (float l, float w, float h)
	calculate the volume of a rectangular prism

mathPlus.VolumeOfSphere (float r)
	calculate the volume of a sphere

mathPlus.VolumeOfCone (float BaseArea, float h)
	calculate the volume of a cone or pyrimid

mathPlus.VolumeOfCube (float l)
	calculate the volume of a cube

mathPlus.VolumeOfCylinder (float BaseArea, float h)
	calculate the volume of a cylinder

mathPlus.PerimeterOfSquare (float s)
	calculate the perimeter of a square

mathPlus.PerimeterOfRectangle (float l, float w)
	calculate the perimeter of a rectangle

mathPlus.PerimeterOfTriangle (float a, float b, float c)
	calculate the perimeter of a triangle. a, b, and c are side lengths

mathPlus.SortIntArray(int[] Array)
	sorts all the values in an int array least to greatest

mathPlus.SortFloatArray(float[] Array)
	sorts all the values in an float array least to greatest

mathPlus.SortIntArrayReverse(int[] Array)
	sorts all the values in an int array greatest to least

mathPlus.SortFloatArrayReverse(float[] Array)
	sorts all the values in an float array greatest to least

mathPlus.SortStringArrayByLength(string[] String)
	sort all the strings in an array shortest to longest

mathPlus.SortStringArrayByLengthReverse(string[] String)
	sort all the strings in an array longest to shortest

mathPlus.appendVector3Arrays (Vector3[] a, Vector3[] b)
	append 2 vector3 arrays

mathPlus.appendVector2Arrays (Vector2[] a, Vector2[] b)
	append 2 vector3 arrays

mathPlus.CreateCatmullSpline (Vector3[] points, int resolution, bool loop)
	create a spline using catmull