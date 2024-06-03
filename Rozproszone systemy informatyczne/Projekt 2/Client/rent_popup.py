import tkinter as tk
from tkinter import messagebox
from requests.auth import HTTPBasicAuth

import requests

class RentMotorcyclePopup:
    def __init__(self, parent, base_url, cert_path, cert_key, username, password, id, update_callback):
        self.parent = parent
        self.base_url = base_url
        self.cert_path = cert_path
        self.cert_key = cert_key
        self.username = username 
        self.password = password
        self.id = id
        self.update_callback = update_callback
        self.popup = tk.Toplevel(parent)
        self.popup.title("Wypożycz motocykl")

        tk.Label(self.popup, text="Na ile dni chcesz wypożyczyć motocykl?").grid(row=0, column=0)
        self.days_entry = tk.Entry(self.popup)
        self.days_entry.grid(row=0, column=1)

        tk.Button(self.popup, text="Wypożycz", command=self.rent_motorcycle).grid(row=1, column=0, columnspan=2)
        tk.Button(self.popup, text="Anuluj", command=self.popup.destroy).grid(row=2, column=0, columnspan=2)

    def rent_motorcycle(self):
        days = self.days_entry.get()

        try:
            response = requests.post(
                f"{self.base_url}/rent?id={self.id}&numOfDays={days}",
                cert=(self.cert_path, self.cert_key),
                verify=False,
                auth=HTTPBasicAuth(self.username, self.password)
            )
            response.raise_for_status()
            messagebox.showinfo("Sukces", "Motocykl został wypożyczony pomyślnie.")
            self.update_callback()
            self.popup.destroy()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))
