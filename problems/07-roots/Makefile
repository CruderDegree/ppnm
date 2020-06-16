.PHONY : all clean test

all: Answer.txt

Answer.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs $< -reference:lib.dll

lib.dll: lib/Roots.cs lib/matrix.cs lib/vector.cs lib/LinEqs.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt

test: test.exe
	mono $<

test.exe: test.cs lib.dll
	mcs $< -r:lib.dll
