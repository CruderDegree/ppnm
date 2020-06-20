.PHONY : all clean
MATHDIR = ../../math/

all: Answer.txt

Answer.txt: main.exe
	mono $< > $@

main.exe: main.cs quad.dll
	mcs $< -r:quad.dll

quad.dll: $(MATHDIR)Quadratures.cs $(MATHDIR)quado8.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll
