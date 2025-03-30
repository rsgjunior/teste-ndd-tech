import { Component, inject, type OnInit } from "@angular/core"
import { CommonModule } from "@angular/common"
import { MatCardModule } from "@angular/material/card"
import { MatTabsModule } from "@angular/material/tabs"
import { MatButtonModule } from "@angular/material/button"
import { MatDividerModule } from "@angular/material/divider"
import { MatSnackBarModule } from "@angular/material/snack-bar"
import { MatIconModule } from "@angular/material/icon"
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner"
import { RobotService } from "./services/robot.service"
import { RobotPartName, RobotPosition } from "./models/robot.model"
import { ElbowPosition, HeadInclinationPosition, HeadRotationPosition, WristPosition } from "./models/position.enum"
import { PositionTranslatePipe } from './pipes/position-translate.pipe';

@Component({
  selector: "app-root",
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTabsModule,
    MatButtonModule,
    MatDividerModule,
    MatSnackBarModule,
    MatIconModule,
    MatProgressSpinnerModule,
    PositionTranslatePipe,
  ],
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  private robotService = inject(RobotService)

  protected robot = this.robotService.robot
  protected loading = this.robotService.loading

  selectedTabIndex = 0;

  ElbowPosition = ElbowPosition;
  WristPosition = WristPosition;
  HeadRotationPosition = HeadRotationPosition;
  HeadInclinationPosition = HeadInclinationPosition;

  ngOnInit(): void {
    this.robotService.loadRobotState();
  }

  handleMovement(part: RobotPartName, position: RobotPosition): void {
    this.robotService.moveRobotPart(part, position)
  }
}

