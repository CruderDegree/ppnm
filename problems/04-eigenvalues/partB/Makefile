TIMES = 2 4 6 8 10 15 20 25 30 40 50 60 75 100 125 150 175 200

all: Plot-B.svg

Plot-B.svg: plotTime.gpi data-time.txt
	gnuplot $<

data-time.txt: timeMain.exe
	mono $< $(TIMES) > $@
	
timeMain.exe: timeMain.cs lib-time.dll
	mcs $< -reference:lib-time.dll -o:$@

lib-time.dll: ../lib/matrix.cs ../lib/vector.cs ../lib/jacobi.cs ../lib/MatrixMethods.cs
	mcs $^ -target:library -out:$@

test: testing.exe
	mono $<

testing.exe: testing.cs lib-time.dll
	mcs $< -r:lib-time.dll -o:$@

clean:
	rm -f *.exe *.txt *.dll *.svg
