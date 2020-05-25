public interface ivector3d{
	// Properties
	double X{get;set;}
	double Y{get;set;}
	double Z{get;set;}

	// Methods
	double dotP(ivector3d other);
	ivector3d crossP(ivector3d other);
}


