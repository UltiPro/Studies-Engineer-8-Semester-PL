import tkinter as tk
from tkinter import messagebox

import requests


class EditMotorcyclePopup:
    def __init__(self, parent, base_url, cert_path, cert_key, id, update_callback):
        self.parent = parent
        self.base_url = base_url
        self.cert_path = cert_path
        self.cert_key = cert_key
        self.id = id
        self.update_callback = update_callback
        self.popup = tk.Toplevel(parent)
        self.popup.title("Edytuj motocykl")

        try:
            response = requests.get(
                f"{self.base_url}?id={self.id}",
                cert=(self.cert_path, self.cert_key),
                verify=False
            )
            response.raise_for_status()
            motorcycle = response.json()
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

        self.brand_entry.insert(0, motorcycle["brand"])
        self.name_entry.insert(0, motorcycle["name"])
        self.licence_entry.insert(0, motorcycle["requiredLicence"])
        self.price_entry.insert(0, motorcycle["rentPrice"])
        self.description_entry.insert("1.0", motorcycle["description"])

        tk.Button(self.popup, text="Aktualizuj", command=self.update_motorcycle).grid(row=5, column=0, columnspan=2)
        tk.Button(self.popup, text="Anuluj", command=self.popup.destroy).grid(row=6, column=0, columnspan=2)

    def update_motorcycle(self):
        brand = self.brand_entry.get()
        name = self.name_entry.get()
        licence = int(self.licence_entry.get())
        description = self.description_entry.get("1.0", "end")
        price = self.price_entry.get()

        try:
            response = requests.put(
                self.base_url, 
                json={
                    "id": self.id,
                    "brand": brand,
                    "name": name,
                    "requiredLicence": licence,
                    "description": description,
                    "rentPrice": price
                },
                cert=(self.cert_path, self.cert_key),
                verify=False
            )
            response.raise_for_status()
            messagebox.showinfo("Sukces", "Motocykl został zaktualizowany pomyślnie.")
            self.update_callback()
            self.popup.destroy()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))
