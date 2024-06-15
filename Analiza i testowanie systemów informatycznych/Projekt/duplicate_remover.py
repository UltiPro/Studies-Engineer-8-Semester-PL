def process_file(input_file, output_file):
    with open(input_file, "r", encoding="utf-8") as file:
        lines = file.readlines()

    seen_select_lines = set()
    lines_to_keep = []
    i = 0

    while i < len(lines):
        line = lines[i]
        if line.startswith("Select"):
            if line not in seen_select_lines:
                seen_select_lines.add(line)
                lines_to_keep.append(line)
                i += 1
            else:
                # Usunięcie linii poprzedzającej, linii duplikatu i linii następującej
                if i > 0:
                    lines_to_keep.pop()  # Usuń poprzednią linię
                i += 2  # Pomiń bieżącą linię i następną linię
        else:
            lines_to_keep.append(line)
            i += 1

    with open(output_file, "w", encoding="utf-8") as file:
        file.writelines(lines_to_keep)


process_file("simple2_tests.txt", "simple2_tests_output.txt")
