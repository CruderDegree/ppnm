using static System.Math;
using static System.Console;
using System;

public class Jacobi{
	public static (int, int) cyclic(matrix A, matrix V, vector e){
		/* Calculates all eigenvalues and vectors of A
		 * Returns number of sweeps used to calculate these.
		 *
		 * @PARAMS:
		 * A: Square symmetric matrix, upper triangle of A is destroyed
		 * V: Empty square matrix, same size as A. Will Contain eigenvectors
		 * e: Empty vector, same length as A. Will contain eigenvalues
		 */
		int n = A.size1;
		// Turn V into the identity matrix
		for(int i = 0; i<n; i++) V[i,i] = 1;	
		// Convergence criterion
		bool diagChanged;
		// e is a vector containing diagonal elements of A
		for(int i = 0; i<n; i++) e[i] = A[i, i];
		// Perform a Jacobi rotation-sweep and check convergence
		int sweeps = 0, rots = 0;
		do{
			sweeps++;
			diagChanged = true;
			// Loop over upper triang. elements in A
			for(int p = 0; p < n; p++) for(int q = p + 1; q < n; q++){
				rots++;
				double App = e[p]; double Aqq = e[q];
				double Apq = A[p,q];
				// Calculate phi
				double phi = 0.5*Atan2(2*Apq , Aqq - App);
				double c = Cos(phi);
				double s = Sin(phi);

				// Change elements of p,q
				double newApp = c*c*App - 2*c*s*Apq + s*s*Aqq;
				double newAqq = s*s*App + 2*c*s*Apq + c*c*Aqq;
				
				// Check Convergence criterion
				if(newApp != App || newAqq != Aqq){
					// Update diagonal. Zero A[p,q]
					e[p] = newApp; e[q] = newAqq;
					A[p,q] = 0;
					// i = 0 ... p-1 --> ip, iq
					for(int i = 0; i < p; i++){
						double Aip = A[i,p]; double Aiq = A[i,q];
						A[i,p] = c*Aip - s*Aiq;
						A[i,q] = s*Aip + c*Aiq;
					}
					// i = p + 1 ... q-1 --> pi, iq
					for(int i = p+1; i < q; i++){
						double Api = A[p,i]; double Aiq = A[i,q];
						A[p,i] = c*Api - s*Aiq;
						A[i,q] = s*Api + c*Aiq;
					}
					// i = q + 1 ... n-1 --> pi, qi
					for(int i = q+1; i < n; i++){
						double Api = A[p,i]; double Aqi = A[q,i];
						A[p,i] = c*Api - s*Aqi;
						A[q,i] = s*Api + c*Aqi;
					}
	
					// Update V Matrix
					jacobiUpdate(V, p, q, phi);
				}
				else diagChanged = false;
			}
		} while(diagChanged);
		// Procedure is done, A --> D, V has been calculated
		return (sweeps, rots);
	}
	
	public static int classic(matrix A, matrix V, vector e){
		// Setup
		int n = A.size1;
		for(int i = 0; i < n; i++){
			e[i] = A[i,i];
			V[i,i] = 1;
		}

		// Create aux vector with idx of highest value in each row
		int[] h = new int[n - 1]; // n - 1: No upper triang element for last row
		for(int i = 0; i < h.Length; i++) h[i] = findHighest(A,i);

		int rot = 0;
		bool converged = false;
		do{
			converged = true;
			for(int p = 0; p < h.Length; p++){
				rot++;
				int q = h[p];
				double App = e[p]; double Aqq = e[q];
				double Apq = A[p,q];
				// Calculate phi
				double phi = 0.5*Atan2(2*Apq , Aqq - App);
				double c = Cos(phi);
				double s = Sin(phi);

				// Change elements of p,q
				double newApp = c*c*App - 2*c*s*Apq + s*s*Aqq;
				double newAqq = s*s*App + 2*c*s*Apq + c*c*Aqq;
				
				// Check Convergence criterion
				if(newApp != App || newAqq != Aqq){
					// Update diagonal. Zero A[p,q]
					converged = false;
					e[p] = newApp; e[q] = newAqq;
					A[p,q] = 0;
					// i = 0 ... p-1 --> ip, iq
					for(int i = 0; i < p; i++){
						double Aip = A[i,p]; double Aiq = A[i,q];
						A[i,p] = c*Aip - s*Aiq;
						A[i,q] = s*Aip + c*Aiq;
					}
					// i = p + 1 ... q-1 --> pi, iq
					for(int i = p+1; i < q; i++){
						double Api = A[p,i]; double Aiq = A[i,q];
						A[p,i] = c*Api - s*Aiq;
						A[i,q] = s*Api + c*Aiq;
					}
					// i = q + 1 ... n-1 --> pi, qi
					for(int i = q+1; i < n; i++){
						double Api = A[p,i]; double Aqi = A[q,i];
						A[p,i] = c*Api - s*Aqi;
						A[q,i] = s*Api + c*Aqi;
					}

					// Update V Matrix
					for(int i = 0; i < n; i++){
						double Vip = V[i,p]; double Viq = V[i,q];
						V[i, p] = c*Vip - s*Viq;
						V[i, q] = c*Viq + s*Vip;
					}

					// Update entries in h vector
					h[p] = findHighest(A, p);
					// update q if q < n-1
					if(q < h.Length) h[q] = findHighest(A, q);
					// Also update column p and q
					// for all i betw. 0 .. n-1
					// iff p > i and q > i
					for(int i = 0; i < h.Length; i++){
						if(i < p){
							if(Abs(A[i,p]) > Abs(A[i, h[i]])) h[i] = p;
						}
						if(i < q){
							if(Abs(A[i,q]) > Abs(A[i, h[i]])) h[i] = q;
						}
					}

				}
			}
		}while(!converged);
		return rot;
	}
	
	private static int findHighest(matrix A, int row){
		// Search upper triang. of A for highest element
		int col = row + 1;
		if(row == A.size1){
			throw new System.ArgumentException($"Row {row} of matrix does not have an upper triangular element.");
		}
		for(int j = col + 1; j < A.size2; j++){
			if(Abs(A[row,j]) > Abs(A[row,col])) col = j;
		}
		return col;
	}

	public static vector lowestK(matrix A, matrix V, vector e, int k){
		/* Calculates lowest k eigenvalues
		 */
		vector res = new vector(k);

		int n = A.size1;
		// Turn V into the identity matrix
		for(int i = 0; i<n; i++) V[i,i] = 1;	
		// e is a vector containing diagonal elements of A
		for(int i = 0; i<n; i++) e[i] = A[i, i];
		// Perform a Jacobi rotation-sweep and check convergence

		for(int p = 0; p < k; p++) {
			clearRow(p, A, V, e, 1);
			res[p] = e[p];
		}
		return res;
	}

	public static vector highestK(matrix A, matrix V, vector e, int k){
		/* Calculates lowest k eigenvalues
		 */
		vector res = new vector(k);

		int n = A.size1;
		// Turn V into the identity matrix
		for(int i = 0; i<n; i++) V[i,i] = 1;	
		// e is a vector containing diagonal elements of A
		for(int i = 0; i<n; i++) e[i] = A[i, i];
		// Perform a Jacobi rotation-sweep and check convergence

		for(int p = 0; p < k; p++) {
			clearRow(p, A, V, e, -1);
			res[p] = e[p];
		}
		return res;
	}

	private static void clearRow(int p, matrix A, matrix V, vector e, double r){
		// Make sweeps in matrix A until off diag. elements
		// in row p are zeroed
		// r = 1 --> Lowest eigvals, r = -1 --> Highest eigvals
		int n = A.size1;
		bool diagChanged = true;
		while(diagChanged){
			diagChanged = false;
			// Loop over upper triang. elements in A
			for(int q = p + 1; q < n; q++){
				double App = e[p]; double Aqq = e[q];
				double Apq = A[p,q];
				// Calculate phi
				double phi = 0.5*Atan2(r*2*Apq , Aqq*r - App*r);
				double c = Cos(phi);
				double s = Sin(phi);

				// Change elements of p,q
				double newApp = c*c*App - 2*c*s*Apq + s*s*Aqq;
				double newAqq = s*s*App + 2*c*s*Apq + c*c*Aqq;
				
				// Check Convergence criterion
				if(newApp != App || newAqq != Aqq){
					diagChanged = true;
					// Update diagonal. Zero A[p,q]
					e[p] = newApp; e[q] = newAqq;
					A[p,q] = 0.0;
					// i = 0 ... p-1 --> ip, iq
					for(int i = 0; i < p; i++){
						double Aip = A[i,p]; double Aiq = A[i,q];
						A[i,p] = c*Aip - s*Aiq;
						A[i,q] = s*Aip + c*Aiq;
					}
					// i = p + 1 ... q-1 --> pi, iq
					for(int i = p+1; i < q; i++){
						double Api = A[p,i]; double Aiq = A[i,q];
						A[p,i] = c*Api - s*Aiq;
						A[i,q] = s*Api + c*Aiq;
					}
					// i = q + 1 ... n-1 --> pi, qi
					for(int i = q+1; i < n; i++){
						double Api = A[p,i]; double Aqi = A[q,i];
						A[p,i] = c*Api - s*Aqi;
						A[q,i] = s*Api + c*Aqi;
					}

					// Update V Matrix
					for(int i = 0; i < n; i++){
						double Vip = V[i,p]; double Viq = V[i,q];
						V[i, p] = c*Vip - s*Viq;
						V[i, q] = c*Viq + s*Vip;
					}
				}
			}
		}
	}

	public static int twoSidedJacobiSVD(matrix A, matrix U, matrix V){
		/* Two sided Jacobi SVD
		 * Turns A --> D, where A(old) = UDV^T
		 * Parameters:
		 * matrix A: real, square matrix to make SVD on, A --> D when finished
		 * matrix U: square, empty when starting, when finished, A = UDV^T and U*U^T = 1
		 * matrix V: Same as for U
		 */
		int n = A.size1;
		bool converged;
		// Make U and V identity matricies
		for(int i = 0; i < n; i++) {
			U[i,i] = 1;
			V[i,i] = 1;
		}

		// Store old diagonal elements for comparison
		vector d = new vector(n);
		for(int i = 0; i < n; i++) d[i] = A[i,i];
		int sweeps = 0;
		double eps = 1.0/10000;
		do{
			sweeps++;
			converged = true;
			// Perform sweep over all non-diag elements
			for(int p = 0; p < n; p++) for(int q = 0; q < n; q++) if(p != q){
				// Perform a rotation and update U and V
				givensRot(A, U, p, q);
				//jacobiRot(A, U, V, p, q);
				double phi = jacobiRotation(A, p, q);
				jacobiUpdate(U, p, q, phi);
				jacobiUpdate(V, p, q, phi);
			}
			
			// Check convergence after sweep and update diag vector
			for(int i = 0; i < n; i++){
				if(Abs(d[i] - A[i,i]) > eps){
					converged = false;
					d[i] = A[i,i];
				}
			}
		} while(!converged);
	
		// Ensure positive diagonal
		for(int i = 0; i < n; i++) if(A[i,i] < 0){
			A[i,i] *= -1;
			for(int j = 0; j < n; j++) U[j,i] *= -1;
		}

		return sweeps;
	}

	private static void givensRot(matrix A, matrix U, int p, int q){
		// Perform a rotation on A: G^T*A and updates U --> U*G = U'
		double App = A[p,p], Aqq = A[q,q], Apq = A[p,q], Aqp = A[q,p];
		double theta = Atan2(Aqp - Apq, App + Aqq);
		double c = Cos(theta), s = Sin(theta);
		for(int i = 0; i < A.size2; i++){
			// A --> A' = G^T * A
			double Api = A[p,i], Aqi = A[q,i];
			A[p, i] = c*Api + s*Aqi;
			A[q, i] = c*Aqi - s*Api;

			// U --> U' = U*G
			double Uip = U[i,p], Uiq = U[i,q];
			U[i, p] = c*Uip + s*Uiq;
			U[i, q] = c*Uiq - s*Uip;
		}
	}

	public static double jacobiRotation(matrix A, int p, int q){
		// Make a rotation on A to zero elements Apq, Aqp
		// and return rotation angle to update other objects
		double App = A[p,p], Aqq = A[q,q], Apq = A[p,q], Aqp = A[q,p];
		double phi = 0.5*Atan2(Aqp + Apq, Aqq - App);
		double c = Cos(phi), s = Sin(phi);
		// Update off-diag elements of A
		for(int i = 0; i < A.size2; i++) if(i!= p && i!= q){
			// A --> A' = J^T A J
			double Api = A[p,i], Aip = A[i,p];
			double Aqi = A[q,i], Aiq = A[i,q];

			A[p,i] = c*Api - s*Aqi;
			A[q,i] = c*Aqi + s*Api;
			A[i,p] = c*Aip - s*Aiq;
			A[i,q] = c*Aiq + s*Aip;
		}
		// Calc Aqq and App
		A[p,p] = c*c*App + s*s*Aqq - 2*c*s*Aqp;
		A[q,q] = c*c*Aqq + s*s*App + 2*c*s*Apq;

		// Zero Apq = Aqp
		// Actual calculation
		//A[p,q] = s*c*(App - Aqq) + (c*c - s*s)*Apq; 
		//A[q,p] = s*c*(App - Aqq) + (c*c - s*s)*Aqp;
		// Dirty shortcut
		A[p,q] = 0; A[q,p] = 0;
		return phi;
	}
	
	public static void jacobiUpdate(matrix V, int p, int q, double theta){
		// Calculates V*J, where J is the pq Jacobi matrix with angle theta
		double c = Cos(theta), s = Sin(theta);
		// Update V 
		for(int i = 0; i < V.size2; i++){
			double Vip = V[i,p], Viq = V[i,q];
			V[i,p] = c*Vip - s*Viq;
			V[i,q] = c*Viq + s*Vip;
		}	
	}
}
