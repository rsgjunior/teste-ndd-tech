import { ElbowPosition, HeadInclinationPosition, HeadRotationPosition, WristPosition } from "./position.enum";
import { GETRobotApiResponse } from "./robot-api.model";

export abstract class RobotPart<T> {
    currentPosition: T;

    constructor(startPosition: T) {
        this.currentPosition = startPosition;
    }

    abstract getMoveSet(): { [key: string]: T };
}

export class Wrist extends RobotPart<WristPosition> {
    override getMoveSet(): { [key: string]: WristPosition; } {
        return WristPosition;
    }
}

export class Elbow extends RobotPart<ElbowPosition> {
    override getMoveSet(): { [key: string]: ElbowPosition; } {
        return ElbowPosition;
    }
}

export class HeadRotation extends RobotPart<HeadRotationPosition> {
    override getMoveSet(): { [key: string]: HeadRotationPosition; } {
        return HeadRotationPosition;
    }
}

export class HeadInclination extends RobotPart<HeadInclinationPosition> {
    override getMoveSet(): { [key: string]: HeadInclinationPosition; } {
        return HeadInclinationPosition;
    }
}

export class Head {
    rotation: HeadRotation;
    inclination: HeadInclination;

    constructor(rotationStartPosition: HeadRotationPosition, inclinationStartPosition: HeadInclinationPosition) {
        this.rotation = new HeadRotation(rotationStartPosition);
        this.inclination = new HeadInclination(inclinationStartPosition);
    }
}

export class Arm {
    wrist: Wrist;
    elbow: Elbow;

    constructor(wristStartPosition: WristPosition, elbowStartPosition: ElbowPosition) {
        this.wrist = new Wrist(wristStartPosition);
        this.elbow = new Elbow(elbowStartPosition);
    }
}

export class Robot {
    leftArm: Arm;
    rightArm: Arm;
    head: Head;

    constructor() {
        this.leftArm = new Arm(WristPosition.Rest, ElbowPosition.Rest);
        this.rightArm = new Arm(WristPosition.Rest, ElbowPosition.Rest);
        this.head = new Head(HeadRotationPosition.Rest, HeadInclinationPosition.Rest);
    }

    static fromAPIResponse(data: GETRobotApiResponse): Robot {
        const robot = new Robot();

        robot.head = new Head(data.head.rotation.currentPosition, data.head.inclination.currentPosition);
        robot.leftArm = new Arm(data.leftArm.wrist.currentPosition, data.leftArm.elbow.currentPosition);
        robot.rightArm = new Arm(data.rightArm.wrist.currentPosition, data.rightArm.elbow.currentPosition);

        return robot;
    }
}

export type RobotPartName = "leftElbow" | "rightElbow" | "leftWrist" | "rightWrist" | "headRotation" | "headInclination"
export type RobotPosition = ElbowPosition | WristPosition | HeadRotationPosition | HeadInclinationPosition
