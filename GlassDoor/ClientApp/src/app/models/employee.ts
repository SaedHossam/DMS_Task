import { Gender } from "./enums/gender.enum";


export class Employee {


  constructor(
    public id?: number, 
    public userId?: string, 
    public birthDate?: Date, 
    public gender?: Gender,
    public cityName?: string, 
    public countryId?: number,
    public countryName?: string, 
    public isWillingToRelocate?: boolean,
    public mobileNumber?: string, 
    public alternativeMobileNumber?: string, 
    public careerLevelId?: number,
    public careerLevelName?: string,
    public minimumSalary?: number, 
    public experienceYears?: number, 
    public skillId?: number,
    public cV?: string,
    public photo?: string, 
    public summary?: string, 
    public educationLevelId?: number,
    public educationLevelName?: string, 
    public nationalityId?: number, 
    public nationalityName?: string,
    public userFirstName?: string, 
    public userLastName?: string, 
    public email?: string) { }

}
