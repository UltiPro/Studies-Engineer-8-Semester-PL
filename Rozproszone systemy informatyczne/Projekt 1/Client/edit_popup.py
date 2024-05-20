import tkinter as tk
from tkinter import messagebox


class EditMotorcyclePopup:
    def __init__(self, parent, client, id, update_callback):
        self.parent = parent
        self.client = client
        self.id = id
        self.update_callback = update_callback
        self.popup = tk.Toplevel(parent)
        self.popup.title("Edytuj motocykl")

        try:
            motorcycle = self.client.service.Detail(id)
        except Exception as e:
            messagebox.showerror("Błąd", str(e))
            self.popup.destroy()
            return

        tk.Label(self.popup, text="Marka:").grid(row=0, column=0, padx=10, pady= 5)
        self.brand_entry = tk.Entry(self.popup)
        self.brand_entry.grid(row=0, column=1, padx=10, pady= 5)

        tk.Label(self.popup, text="Nazwa:").grid(row=1, column=0, padx=10, pady= 5)
        self.name_entry = tk.Entry(self.popup)
        self.name_entry.grid(row=1, column=1, padx=10, pady= 5)

        tk.Label(self.popup, text="Wymagane prawo jazdy:").grid(row=2, column=0, padx=10, pady= 5)
        self.licence_entry = tk.Entry(self.popup)
        self.licence_entry.grid(row=2, column=1, padx=10, pady= 5)

        tk.Label(self.popup, text="Cena wynajmu:").grid(row=3, column=0, padx=10, pady= 5)
        self.price_entry = tk.Entry(self.popup)
        self.price_entry.grid(row=3, column=1, padx=10, pady= 5)

        tk.Label(self.popup, text="Opis:").grid(row=4, column=0, padx=10, pady= 5)
        self.description_entry = tk.Entry(self.popup)
        self.description_entry = tk.Text(self.popup, wrap=tk.WORD, width=80, height=10)
        self.description_entry.grid(row=4, column=1, padx=10, pady= 5)

        self.brand_entry.insert(0, motorcycle["Brand"])
        self.name_entry.insert(0, motorcycle["Name"])
        self.licence_entry.insert(0, motorcycle["RequiredLicence"])
        self.price_entry.insert(0, motorcycle["RentPrice"])
        self.description_entry.insert("1.0", motorcycle["Description"])

        tk.Button(self.popup, text="Aktualizuj", command=self.update_motorcycle).grid(row=5, column=0, columnspan=2)
        tk.Button(self.popup, text="Anuluj", command=self.popup.destroy).grid(row=6, column=0, columnspan=2)

    def update_motorcycle(self):
        brand = self.brand_entry.get()
        name = self.name_entry.get()
        licence = self.licence_entry.get()
        description = self.description_entry.get("1.0", "end")
        price = self.price_entry.get()

        try:
            self.client.service.Update({
                "Id": self.id,
                "Brand": brand,
                "Name": name,
                "RequiredLicence": licence,
                "Description": description,
                "RentPrice": price
            })
            messagebox.showinfo("Sukces", "Motocykl został zaktualizowany pomyślnie.")
            self.update_callback()
            self.popup.destroy()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))