import tkinter as tk
from tkinter import messagebox


class AddMotorcyclePopup:
    def __init__(self, parent, client, update_callback):
        self.parent = parent
        self.client = client
        self.update_callback = update_callback
        self.popup = tk.Toplevel(parent)
        self.popup.title("Dodaj motocykl")

        tk.Label(self.popup, text="Marka:").grid(row=0, column=0)
        self.brand_entry = tk.Entry(self.popup)
        self.brand_entry.grid(row=0, column=1)

        tk.Label(self.popup, text="Nazwa:").grid(row=1, column=0)
        self.name_entry = tk.Entry(self.popup)
        self.name_entry.grid(row=1, column=1)

        tk.Label(self.popup, text="Wymagane prawo jazdy:").grid(row=2, column=0)
        self.licence_entry = tk.Entry(self.popup)
        self.licence_entry.grid(row=2, column=1)

        tk.Label(self.popup, text="Opis:").grid(row=3, column=0)
        self.description_entry = tk.Entry(self.popup)
        self.description_entry.grid(row=3, column=1)

        tk.Label(self.popup, text="Cena wynajmu:").grid(row=4, column=0)
        self.price_entry = tk.Entry(self.popup)
        self.price_entry.grid(row=4, column=1)

        tk.Button(self.popup, text="Dodaj", command=self.add_motorcycle).grid(row=5, column=0, columnspan=2)
        tk.Button(self.popup, text="Anuluj", command=self.popup.destroy).grid(row=6, column=0, columnspan=2)

    def add_motorcycle(self):
        brand = self.brand_entry.get()
        name = self.name_entry.get()
        licence = self.licence_entry.get()
        description = self.description_entry.get()
        price = self.price_entry.get()

        try:
            self.client.service.Create({
                "Brand": brand,
                "Name": name,
                "RequiredLicence": licence,
                "Description": description,
                "RentPrice": price
            })
            messagebox.showinfo("Sukces", "Motocykl został dodany pomyślnie.")
            self.update_callback()
            self.popup.destroy()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))