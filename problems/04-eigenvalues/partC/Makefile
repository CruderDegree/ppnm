all: Plot-C.svg

Plot-C.svg: plot-c.gpi data-C.txt
	gnuplot $<

data-C.txt: main-C.exe
	mono $< > $@

main-C.exe: main-C.cs libC.dll
	mcs $< -r:libC.dll -o:$@

libC.dll: ../lib/jacobi.cs ../lib/matrix.cs ../lib/MatrixMethods.cs ../lib/vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt
