.PHONY : all testA clean

all: testA

testA: partA.exe
	mono $<

partA.exe: partA.cs libA.dll
	mcs $< -r:libA.dll

libA.dll: ../lib/Ode.cs ../lib/vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.dll
