using static System.Math;
using static System.Random;
using System;
using static System.Console;

public class MonteCarlo{
	public static (double, double) plainMC(
			Func<vector, double> f,	vector a, vector b, int N){
		// Construct random X vector
		Func<vector, vector, vector> randX = delegate(vector v1, vector v2){
			Random rand = new Random();
			int n = v1.size;
			vector x = new vector(n);
			for(int i = 0; i < n; i++) x[i] = v1[i] + rand.NextDouble() * (v2[i] - v1[i]); 
			return x;
		};
		// Calculate volume
		double volume = 1; 
		for(int i = 0; i < a.size; i++) volume *= b[i] - a[i];
		// Calculate sum and squared sum
		double sum = 0, sum2 = 0;
		for(int i = 0; i < N; i++){
			double fx = f(randX(a,b));
			sum += fx;
			sum2 += fx*fx;
		}

		double mean = sum/N;
		double sigma = Sqrt(sum2 / N - mean*mean);
		double SIGMA = sigma / Sqrt(N);
		
		return (mean*volume, SIGMA*volume);
	}

	public static (double, double) recStratifMC(Func<vector, double> f, vector a, vector b, int N, double acc = 1e-4){
		//WriteLine("Recursive MC called!");
		// Sample N points from plain MC
		double plainRes, plainError;
		(plainRes, plainError) = plainMC(f, a, b, N);
		// If error is good, we return plain MC
		if(plainError < acc) return (plainRes, plainError);
		else{
			int n = a.size; // # of dimensions
			int k = 0;// while going over dimensions, we search for the dimension with biggest subvariance
			double maxSoFar = 0;
			double res1, res2, error1, error2, V = 1;
			for(int i = 0; i < n; i++) V *= b[i]-a[i]; // Calculate volume
			// For each dimension
			for(int i = 0; i < n; i++){
				// Supdivide  the volume in 2 along the dimension
				// first volume
				vector mid = b.copy();
				mid[i] = a[i] + (b[i] - a[i]) / 2;
				// Estimate sub-variances along these 2 dimensions;
				(res1, error1) = plainMC(f, a, mid, N/2);
				double variance1 = error1*error1*N/2/V/V/4; // From central limit theorem (eq 3)
				// Second volume
				mid = a.copy();
				mid[i] = a[i] + (b[i] - a[i])/2;
				(res2, error2) = plainMC(f, mid, b, N/2);
				double variance2 = error2*error2*N/2/V/V/4; // From central limit theorem (eq 3)
				// Check only largest of the 2 variances
				double maxForI = Max(variance1, variance2);
				if(maxForI > maxSoFar) k = i;
			}
			// We now know the dimension with largest sub-variance
			vector midk = b.copy();
			midk[k] = a[k] + (b[k] - a[k])/2;
			// Subdivide volume along this dimension
			// and make 2 recursive calls to these divisions
			(res1, error1) = recStratifMC(f, a, midk, N/2);
			midk = a.copy();
			midk[k] = a[k] + (b[k] - a[k])/2;
			(res2, error2) = recStratifMC(f, midk, b, N/2);
			// Estimate grand error and grand average
			// This is from Wiki(MISER M-C)
			double gRes = (res1 + res2)/2;
			double gErr = (error1 + error2)/4 /N;
			return (gRes, gErr);
		}
	}
}
