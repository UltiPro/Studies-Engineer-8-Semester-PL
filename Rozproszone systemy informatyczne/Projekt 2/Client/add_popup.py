import tkinter as tk
from tkinter import messagebox
from requests.auth import HTTPBasicAuth
import requests

class AddMotorcyclePopup:
    def __init__(self, parent, base_url, cert_path, cert_key, username, password, update_callback):
        self.parent = parent
        self.base_url = base_url
        self.cert_path = cert_path
        self.cert_key = cert_key
        self.username = username
        self.password = password
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
        licence = int(self.licence_entry.get())
        description = self.description_entry.get()
        price = self.price_entry.get()

        try:
            print(f'cert_key: {self.cert_key} \n cert_path: {self.cert_path}')
            response = requests.post(
                self.base_url,
                json={
                    "brand": brand,
                    "name": name,
                    "requiredLicence": licence,
                    "description": description,
                    "rentPrice": price
                },
                cert=(self.cert_path, self.cert_key),
                verify=False,
                auth=HTTPBasicAuth(self.username, self.password)
            )
            response.raise_for_status()
            messagebox.showinfo("Sukces", "Motocykl został dodany pomyślnie.")
            self.update_callback()
            self.popup.destroy()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

