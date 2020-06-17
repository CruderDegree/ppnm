LIBFILES = lib/vector.cs lib/MonteCarlo.cs

.PHONY : all clean test

all: Answer.txt

Answer.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs $< -r:lib.dll -out:$@

lib.dll: $(LIBFILES)
	mcs $^ -target:library -out:$@

clean: 
	rm -f *.dll *.txt *.exe

