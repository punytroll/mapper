default: all

ASSEMBLY_REFERENCES = \
	System.Drawing \
	System.Windows.Forms

EXECUTABLE = Map

SOURCE_FILES = $(sort $(shell find . -name '*.cs'))

all: all-recursive all-here

all-here: $(EXECUTABLE)

all-recursive:

clean:
	$(RM) $(EXECUTABLE)

$(EXECUTABLE): $(SOURCE_FILES)
	mcs $^ $(patsubst %,-reference:%,$(ASSEMBLY_REFERENCES)) -out:$@

.PHONY: all all-here all-recursive
