import { Injectable, signal, inject } from "@angular/core"
import { HttpClient } from "@angular/common/http"
import { Robot, RobotPartName, RobotPosition } from "../models/robot.model"
import { catchError, finalize, tap } from "rxjs/operators"
import { throwError } from "rxjs"
import { GETRobotApiResponse } from "../models/robot-api.model"
import { MatSnackBar } from "@angular/material/snack-bar"

@Injectable({
    providedIn: "root",
})
export class RobotService {
    private snackBar = inject(MatSnackBar);
    private http = inject(HttpClient);
    private apiUrl = "http://localhost:5070/robo";

    // Signals para gerenciar o estado
    robot = signal<Robot>(new Robot());
    loading = signal<boolean>(false);

    loadRobotState(): void {
        this.loading.set(true)

        this.http
            .get<any>(this.apiUrl)
            .pipe(
                tap((data) => {
                    this.robot.set(Robot.fromAPIResponse(data))
                }),
                catchError((err) => {
                    console.error("Erro ao carregar o estado do robô:", err)

                    this.snackBar.open("Falha ao carregar o estado do robô.", "Fechar", {
                        duration: 5000,
                    })

                    return throwError(() => err)
                }),
                finalize(() => {
                    this.loading.set(false)
                }),
            )
            .subscribe()
    }

    moveRobotPart(part: RobotPartName, position: RobotPosition) {
        this.loading.set(true)

        let endpoint: string
        let payload: { position: RobotPosition }

        switch (part) {
            case "leftElbow":
            case "rightElbow":
                endpoint = `${this.apiUrl}/arm/${part === "leftElbow" ? "left" : "right"}/elbow`
                payload = { position }
                break
            case "leftWrist":
            case "rightWrist":
                endpoint = `${this.apiUrl}/arm/${part === "leftWrist" ? "left" : "right"}/wrist`
                payload = { position }
                break
            case "headRotation":
                endpoint = `${this.apiUrl}/head/rotate`
                payload = { position }
                break
            case "headInclination":
                endpoint = `${this.apiUrl}/head/incline`
                payload = { position }
                break
            default:
                throw new Error("Parte do robô inválida")
        }

        return this.http
            .put<GETRobotApiResponse>(endpoint, payload)
            .pipe(
                tap((response) => {
                    this.robot.set(Robot.fromAPIResponse(response))
                    this.snackBar.open('Posição do robô atualizada!', 'Fechar', {
                        duration: 2000,
                    });
                }),
                catchError((err) => {
                    console.error("Erro ao mover parte do robô:", err)

                    this.snackBar.open("Falha ao mover o robô", "Fechar", {
                        duration: 5000,
                    })

                    return throwError(() => err)
                }),
                finalize(() => {
                    this.loading.set(false)
                }),
            )
            .subscribe()

    }
}