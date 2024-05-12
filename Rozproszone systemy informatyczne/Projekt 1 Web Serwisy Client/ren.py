from datetime import datetime
import os
import tkinter as tk
from tkinter import ttk, messagebox
from zeep import Client
from zeep.transports import Transport
import requests
import subprocess

from add_popup import AddMotorcyclePopup
from edit_popup import EditMotorcyclePopup
from rent_popup import RentMotorcyclePopup

class MotorcycleRentalApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Motorcycle Rental App")
        self.current_brand_filter = ""
        self.create_gui()

        # Inicjalizacja klienta SOAP
        session = requests.Session()
        session.verify = "./cert.crt"
        self.client = Client(wsdl="https://localhost:7107/Motor.wsdl", transport=Transport(session=session))

        self.display_motorcycles()

    def create_gui(self):
        filter_frame = tk.Frame(self.root)
        filter_frame.grid(row=0, column=0, sticky="ew", padx=10, pady=5)
        
        tk.Label(filter_frame, text="Filtruj po marce:").grid(row=0, column=1, sticky="w", padx=5, pady=5)
        self.brand_filter_entry = tk.Entry(filter_frame)
        self.brand_filter_entry.grid(row=0, column=2, sticky="ew", padx=5, pady=5) 
        tk.Button(filter_frame, text="Filtruj", command=self.filter_motorcycles).grid(row=0, column=3, sticky="e", padx=5, pady=5)

        tk.Button(filter_frame, text="Dodaj motocykl", command=self.open_add_motorcycle_popup).grid(row=0, column=0, sticky="e", padx=(10, 250), pady=5)


        table_frame = tk.Frame(self.root)
        table_frame.grid(row=1, column=0, sticky="nsew", padx=10, pady=(0, 10))

        self.table_frame = tk.Frame(table_frame)
        self.table_frame.pack(fill="both", expand=True)
        
    def open_add_motorcycle_popup(self):
        popup = AddMotorcyclePopup(self.root, self.client, self.display_motorcycles)

    def display_motorcycles(self):
        try:
            motorcycles = self.client.service.GetAll()

            for widget in self.table_frame.winfo_children():
                widget.destroy()

            tk.Label(self.table_frame, text="ID", font=('Arial', 10, 'bold')).grid(row=0, column=0, padx=5, pady=5)
            tk.Label(self.table_frame, text="Marka", font=('Arial', 10, 'bold')).grid(row=0, column=1, padx=5, pady=5)
            tk.Label(self.table_frame, text="Nazwa", font=('Arial', 10, 'bold')).grid(row=0, column=2, padx=5, pady=5)
            tk.Label(self.table_frame, text="Wymagane prawo jazdy", font=('Arial', 10, 'bold')).grid(row=0, column=3, padx=5, pady=5)
            tk.Label(self.table_frame, text="Wynajęty do",font=('Arial', 10, 'bold')).grid(row=0, column=4, padx=5, pady=5)
            tk.Label(self.table_frame, text="Rezerwacja", font=('Arial', 10, 'bold')).grid(row=0, column=5, padx=5, pady=5) 
            tk.Label(self.table_frame, text="Cena za dobę", font=('Arial', 10, 'bold')).grid(row=0, column=6, padx=5, pady=5)
            tk.Label(self.table_frame, text="Opcje", font=('Arial', 10, 'bold')).grid(row=0, column=7, padx=5, pady=5)

            # Dodanie danych do tabeli
            for i, motor in enumerate(motorcycles):
                if self.current_brand_filter and self.current_brand_filter != motor["Brand"]:
                    continue
                tk.Label(self.table_frame, text=motor["Id"]).grid(row=i+1, column=0, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["Brand"]).grid(row=i+1, column=1, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["Name"]).grid(row=i+1, column=2, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["RequiredLicence"]).grid(row=i+1, column=3, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["RentTo"]).grid(row=i+1, column=4, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["Reservation"]).grid(row=i+1, column=5, padx=5, pady=5)
                tk.Label(self.table_frame, text=str(motor["RentPrice"]) + " zł").grid(row=i+1, column=6, padx=5, pady=5)
                options_frame = tk.Frame(self.table_frame)
                options_frame.grid(row=i+1, column=7, padx=5, pady=5)
                tk.Button(options_frame, text="Usuń", command=lambda id=motor["Id"]: self.remove_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Edytuj", command=lambda id=motor["Id"]: self.edit_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Szczegóły", command=lambda id=motor["Id"]: self.show_motorcycle_details(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Rezerwuj", command=lambda id=motor["Id"]: self.reserve_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Anuluj rezerwację", command=lambda id=motor["Id"]: self.cancel_reservation(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Wypożycz", command=lambda id=motor["Id"]: self.rent_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="PDF", command=lambda id=motor["Id"]: self.generate_pdf(id)).pack(side=tk.LEFT, padx=2)
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def filter_motorcycles(self):
        self.current_brand_filter = self.brand_filter_entry.get()
        try:
            motorcycles = self.client.service.GetSelected(self.current_brand_filter)

            for widget in self.table_frame.winfo_children():
                widget.destroy()

            tk.Label(self.table_frame, text="ID", font=('Arial', 10, 'bold')).grid(row=0, column=0, padx=5, pady=5)
            tk.Label(self.table_frame, text="Marka", font=('Arial', 10, 'bold')).grid(row=0, column=1, padx=5, pady=5)
            tk.Label(self.table_frame, text="Nazwa", font=('Arial', 10, 'bold')).grid(row=0, column=2, padx=5, pady=5)
            tk.Label(self.table_frame, text="Wymagane prawo jazdy", font=('Arial', 10, 'bold')).grid(row=0, column=3, padx=5, pady=5)
            tk.Label(self.table_frame, text="Wynajęty do",font=('Arial', 10, 'bold')).grid(row=0, column=4, padx=5, pady=5)
            tk.Label(self.table_frame, text="Rezerwacja", font=('Arial', 10, 'bold')).grid(row=0, column=5, padx=5, pady=5) 
            tk.Label(self.table_frame, text="Cena za dobę", font=('Arial', 10, 'bold')).grid(row=0, column=6, padx=5, pady=5)
            tk.Label(self.table_frame, text="Opcje", font=('Arial', 10, 'bold')).grid(row=0, column=7, padx=5, pady=5)

            # Dodaj nowe wpisy
            for i, motor in enumerate(motorcycles):
                tk.Label(self.table_frame, text=motor["Id"]).grid(row=i+1, column=0, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["Brand"]).grid(row=i+1, column=1, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["Name"]).grid(row=i+1, column=2, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["RequiredLicence"]).grid(row=i+1, column=3, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["RentTo"]).grid(row=i+1, column=4, padx=5, pady=5)
                tk.Label(self.table_frame, text=motor["Reservation"]).grid(row=i+1, column=5, padx=5, pady=5)
                tk.Label(self.table_frame, text=str(motor["RentPrice"]) + " zł").grid(row=i+1, column=6, padx=5, pady=5)
                options_frame = tk.Frame(self.table_frame)
                options_frame.grid(row=i+1, column=7, padx=5, pady=5)
                tk.Button(options_frame, text="Usuń", command=lambda id=motor["Id"]: self.remove_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Edytuj", command=lambda id=motor["Id"]: self.edit_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Szczegóły", command=lambda id=motor["Id"]: self.show_motorcycle_details(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Rezerwuj", command=lambda id=motor["Id"]: self.reserve_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Anuluj rezerwację", command=lambda id=motor["Id"]: self.cancel_reservation(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="Wypożycz", command=lambda id=motor["Id"]: self.rent_motorcycle(id)).pack(side=tk.LEFT, padx=2)
                tk.Button(options_frame, text="PDF", command=lambda id=motor["Id"]: self.generate_pdf(id)).pack(side=tk.LEFT, padx=2)
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def remove_motorcycle(self, id):
        try:
            self.client.service.Remove(id)
            messagebox.showinfo("Sukces", "Motocykl został usunięty pomyślnie.")
            self.display_motorcycles()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def edit_motorcycle(self, id):
        popup = EditMotorcyclePopup(self.root, self.client, id, self.display_motorcycles)

    def show_motorcycle_details(self, id):
        try:
            details = self.client.service.Detail(id)

            details_window = tk.Toplevel(self.root)
            details_window.title("Szczegóły motocykla")

            label_names = {
                'Id': 'ID',
                'Brand': 'Marka',
                'Name': 'Nazwa',
                'RequiredLicence': 'Wymagane prawo jazdy',
                'RentPrice': 'Cena za dobę',
                'RentTo': 'Wynajęty do',
                'Reservation': 'Rezerwacja'
            }

            for i, (key, label) in enumerate(label_names.items()):
                tk.Label(details_window, text=label, font=('Arial', 10, 'bold')).grid(row=0, column=i, padx=5, pady=5)
                if key == 'RentPrice':
                    tk.Label(details_window, text=str(details[key]) + " zł").grid(row=1, column=i, padx=5, pady=5)
                else:
                    tk.Label(details_window, text=details[key]).grid(row=1, column=i, padx=5, pady=5)

            description_text = tk.Text(details_window, wrap=tk.WORD, width=80, height=10)
            description_text.insert(tk.END, details['Description'])
            description_text.config(state=tk.DISABLED)  
            description_text.grid(row=2, columnspan=len(label_names), padx=5, pady=5)

        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def reserve_motorcycle(self, id):
        try:
            self.client.service.Reserve(id)
            messagebox.showinfo("Sukces", "Motocykl został zarezerwowany pomyślnie.")
            self.display_motorcycles()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def cancel_reservation(self, id):
        try:
            self.client.service.CancelReserve(id)
            messagebox.showinfo("Sukces", "Rezerwacja motocykla została anulowana pomyślnie.")
            self.display_motorcycles()
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

    def rent_motorcycle(self, id):
        popup = RentMotorcyclePopup(self.root, self.client, id, self.display_motorcycles)

    def generate_pdf(self, id):
        try:
            response = self.client.service.GeneratePDF(id)

            current_datetime = datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
            file_name = f"Motorcycle_{id}_{current_datetime}.pdf"
            folder_path = "./PDF"

            if not os.path.exists(folder_path):
                os.makedirs(folder_path)
            file_path = os.path.join(folder_path, file_name)

            with open(file_path, 'wb') as file:
                file.write(response)
            subprocess.Popen([os.path.abspath(file_path)], shell=True)
            messagebox.showinfo("Sukces", f"PDF został wygenerowany pomyślnie. Plik zapisano jako: {file_path}")
        except Exception as e:
            messagebox.showerror("Błąd", str(e))

if __name__ == "__main__":
    root = tk.Tk()
    app = MotorcycleRentalApp(root)
    root.mainloop()