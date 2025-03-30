import { ElbowPosition, HeadInclinationPosition, HeadRotationPosition, WristPosition } from "./position.enum";

export interface GETRobotApiResponse {
    rightArm: {
        wrist: {
            currentPosition: WristPosition;
        };
        elbow: {
            currentPosition: ElbowPosition;
        };
    };
    leftArm: {
        wrist: {
            currentPosition: WristPosition;
        };
        elbow: {
            currentPosition: ElbowPosition;
        };
    };
    head: {
        rotation: {
            currentPosition: HeadRotationPosition;
        };
        inclination: {
            currentPosition: HeadInclinationPosition;
        };
    };
}