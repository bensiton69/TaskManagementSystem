export interface Task {
    id: string;
    title: string;
    description: string;
    status: number;
    urgentLevel: number;
    ownerId: string;
    deadline: string;
}