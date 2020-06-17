using static System.Math;
using static System.Random;
using System;

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
}
