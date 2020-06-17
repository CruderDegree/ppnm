LIBFILES = lib/LinEqs.cs lib/matrix.cs lib/vector.cs lib/Minimization.cs

.PHONY : all clean

all: Answer.txt

Answer.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs $< -r:lib.dll -out:$@

lib.dll: $(LIBFILES)
	mcs $^ -target:library -out:$@

clean: *.dll *.txt *.exe
