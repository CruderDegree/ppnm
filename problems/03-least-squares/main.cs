using System;
using static System.Console;
using static System.Math;
using System.IO;

class main{
	static int Main(){
		// Load data (not from file...)
		vector t = new vector(new double[] {1, 2, 3, 4, 6, 9, 10, 13, 15});
		vector A = new vector(new double[] {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1});
		vector dA = new vector(A.size); // uncertainty of A
		for(int i=0; i<A.size; i++) dA[i] = 0.05; // 5% uncertainty
		
		// Transform data from y = a*exp(-lambda*t) --> ln(y) = ln(a) - lambda*t
		for (int i = 0; i<A.size; i++){
			dA[i] = dA[i]/A[i]; // delta ln(y) --> (delta y )/ y
			A[i] = Log(A[i]);
		}

		// Make function array
		Func<double,double>[] funcs = new Func<double,double>[] {x => 1, x => x}; 
		// t -> 1 is for ln(a), t -> t is for lambda
		
		// Make fit
		Fit FitResult = qr_stuff.qrFitter(t, A, dA, funcs);
		vector cErrors = FitResult.getParamErrors();
		double a = Exp(FitResult.c[0]);
		double aErr = cErrors[0];
		double lambda = -FitResult.c[1];
		double lambdaErr = cErrors[1];

		// Generate Answer to answer questions:	
		FitResult.c.print("Fit result parameters: ");
		WriteLine($"This means that a: {a} and lambda: {lambda}");
		WriteLine($"This gives a half life of {Log(2)/lambda} days");
		WriteLine("The half life of Ra224 is actually around 3.6319 days");
		WriteLine("");
		FitResult.cov.print("The Covariance matrix is estimated to");
		cErrors.print("This give the following errors of the parameters");
		WriteLine($"Thus, the half of ThX is estimated to be between {Log(2)/lambda} +- {Log(2)/lambda/lambda*lambdaErr} days");


		// Generate plotting data to plot with
		TextWriter DataWriter = Error;
		//DataWriter.WriteLine("This is in the data stream");
		for(double x = 0; x < 16; x+=1.0/16) DataWriter.WriteLine($"{x} {Exp(FitResult.evaluate(x))} {Exp(FitResult.upper(x))} {Exp(FitResult.lower(x))}");

		return 0;
	}
}
