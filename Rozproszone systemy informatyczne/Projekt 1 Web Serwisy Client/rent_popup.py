import tkinter as tk
from tkinter import messagebox

class RentMotorcyclePopup:
    def __init__(self, parent, client, id, update_callback):
        self.parent = parent
        self.client = client
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
            self.client.service.Rent(self.id, days)
            messagebox.showinfo("Sukces", "Motocykl został wypożyczony pomyślnie.")
            self.update_callback()
            self.popup.destroy()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))