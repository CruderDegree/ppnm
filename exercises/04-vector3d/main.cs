using static System.Console;

class main{
	static void Main(){
		vector3d v = new vector3d(1,0,42);
		WriteLine($"v = {v}");
		vector3d u = new vector3d(42,42,42);
		WriteLine($"u = {u}");
		WriteLine($"v + u = {v+u}");
		WriteLine($"v - u = {v-u}");
		WriteLine($"3*u = {3*u}");
		WriteLine($"u*3 = {u*3}");
		WriteLine($"u/3 = {u/3}");		
		
		WriteLine($"v * u = {v*u}");
		// vector3d s = v.crossP(u);
		WriteLine($"s = v x u = {v.crossP(u)}");
		
		WriteLine($"v * s = {v.dotP(v.crossP(u))}");
		
		WriteLine("Update values test");
		WriteLine($"Old v: {v}");
		v.X = 69; v.Y = 420; v.Z = 100100;	
		WriteLine($"New v: {v}");
		WriteLine($"Dotproduct method test: v*u = {v.dotP(u)}");
	}
}
