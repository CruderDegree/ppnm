using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;

public class Minimization{
	public static (vector, int) qNewton(
			Func<vector,double> f, // Function phi
			vector x0, // Starting point
			double eps // Desired accuracy
			){
		int steps = 0;
		vector x = x0.copy();
		matrix B = resetB(x.size);
		vector dfdx = gradient(f, x);
		while(dfdx.norm() > eps){
			steps++;
			// Calc dx
			vector dx = -1*B*dfdx;
			// Backtracking search
			double lambda = 1;
			
			matrix dxT = new matrix(1, dx.size);
			for(int i = 0; i < dx.size; i++) dxT[0, i] = dx[i]; 
			
			double armijo = (dxT * dfdx)[0]; // Armijo condition
			while(f(x + lambda * dx) > f(x) + lambda*armijo / 10000){
				if(lambda < 1.0/64){
					B = resetB(x.size);
					break;
				}
				lambda /= 2;
			}
			// SR1 update of B
			vector y = gradient(f, x + lambda*dx) - dfdx;
			vector u = lambda * dx - B*y;
			matrix uT = new matrix(1, u.size);
			for(int i = 0; i < u.size; i++) uT[0, i] = u[i];
			double uTy = (uT * y)[0];
			if(Abs(uTy) > 1e-6) B.update(u,u, 1/uTy);

			// Update x, dfdx
			x = x + lambda * dx;
			dfdx = gradient(f, x);
		}
		return (x, steps);
	}

	private static vector gradient(Func<vector, double> f, vector x, double dx = 1e-7){
		// Calculate vector of f at x
		vector p = x.copy();
		vector grad = new vector(x.size);
		for(int i = 0; i < x.size; i++){
			p[i] = p[i] + dx;
			grad[i] = (f(p) - f(x))/dx;
			p[i] = p[i] - dx;
		}
		return grad;
	}

	private static matrix resetB(int n){
		matrix B = new matrix(n,n);
		for(int i = 0; i < n; i++) B[i,i] = 1;
		return B;
	}

	public class DownhillSimplex{
		public int steps{get;}
		private int n, h = 0, l = 0;
		private List<vector> simplex = new List<vector>();
		private double tol;
		private Func<vector, double> f;
		private vector centroid;
		public vector x{get;}
		// Solution when method done

		public DownhillSimplex(Func<vector, double> f, vector x0, double eps){
			this.f = f;
			this.tol = eps;
			this.n = x0.size;
			this.x = x0;
			this.steps = 0;
			makeSimplex();
			do{
				steps++;
				updateSimplex();
				vector reflected = reflection();
				if(f(reflected) < f(simplex[l])){
					vector expanded = expansion();
					if(f(expanded) < f(reflected)) simplex[h] = expanded;
					else simplex[h] = reflected;
				}
				else{
					if(f(reflected) < f(simplex[h])) simplex[h] = reflected;
					else{
						vector contracted = contraction();
						if(f(contracted) < f(simplex[h])) simplex[h] = contracted;
						else reduction();
					}
				}
			}while(size() > tol);
			this.x = simplex[l];
		}

		private void makeSimplex(){
			simplex.Add(x.copy());
			for(int i = 0; i < n; i++){
				vector x0 = x.copy();
				x0[i] = 5*x[i] + 5;
				simplex.Add(x0);
			}
		}

		private void updateSimplex(){
			// Update h and l
			for(int i = 0; i < n + 1; i++){
				vector p = simplex[i];
				if(f(p) > f(simplex[h])) h = i;
				if(f(p) < f(simplex[l])) l = i;
			}
			// Calculate centroid
			centroid = new vector(n);
			for(int i = 0; i < n + 1; i++) if(i!=h){
				centroid += simplex[i];
			}
			centroid /= n;
		}

		private vector reflection(){
			return centroid + (centroid - simplex[h]);
		}

		private vector expansion(){
			return centroid + 2*(centroid - simplex[h]);
		}

		private vector contraction(){
			return centroid + 0.5*(simplex[h] - centroid);
		}

		private void reduction(){
			for(int k = 0; k < n+1; k++) if(k != l){
				simplex[k] = 0.5*(simplex[k] + simplex[l]);
			}
		}

		private double size(){
			double s = 0;
			for(int k = 0; k < n + 1; k++) if(k != l){
				double dist = distance(k);
				if(dist > s) s = dist;
			}
			return s;
		}

		private double distance(int k){
			double s = 0;
			for(int i = 0; i < n; i++){
				s += Pow(simplex[l][i] - simplex[k][i], 2);
			}
			return Sqrt(s);
		}
		
	}
}
