public struct vector3d : ivector3d
{
	// fields and getters
	public double X{get;set;}	
	public double Y{get;set;}	
	public double Z{get;set;}	

	// Constructor
	public vector3d(double a, double b, double c){
		this.X = a;
		this.Y = b;
		this.Z = c;
	}

	// Methods
	public void print(){
		System.Console.WriteLine($"[{X},{Y},{Z}]");
	}
	// interface implementations
	public ivector3d crossP(ivector3d v){
		double s1 = Y*v.Z - Z*v.Y;
		double s2 = Z*v.X - X*v.Z;
		double s3 = X*v.Y - Y*v.X;
		return new vector3d(s1,s2,s3);
	}
	
	public double dotP(ivector3d v){
		return X*v.X + Y*v.Y + Z*v.Z;
	}

	// Operators
	// Addition
	public static vector3d operator +(vector3d u, vector3d v){
		return new vector3d(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
	}
	// Subtraction
	public static vector3d operator -(vector3d u, vector3d v){
		return u + (-1)*v;
	}
	// Scalar product
	public static vector3d operator *(double c, vector3d v){
		return new vector3d(v.X*c, v.Y*c, v.Z*c);
	}
	public static vector3d operator *(vector3d v, double c){
		return c*v;
	}
	// division
	public static vector3d operator /(vector3d v, double c){
		return (1/c)*v;
	}

	// Dotproduct
	public static double operator *(vector3d u, vector3d v){
		return u.X*v.X + u.Y*v.Y + u.Z*v.Z;
	}

	// ToString
	public override string ToString(){
		return $"[{X}, {Y}, {Z}]";
	}
}
